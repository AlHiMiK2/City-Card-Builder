using System;
using System.Collections.Generic;
using _Project.Scripts;
using _Project.Scripts.Handlers;
using Mirror;
using UnityEngine;

public class GameNetworkManager : NetworkManager
{
    [Header("Custom")]
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private PlayerSpawnpoint[] _playerSpawnpoints;

    private Player[] _players;
    
    public const int PlayerCount = 2;

    [Serializable]
    private class PlayerSpawnpoint
    {
        public Player Prefab;
        public Transform Spawnpoint;
    }
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        _players = new Player[PlayerCount];
        UIHandler.Instance.SetWaitingPlayerPanelState(true);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        var spawnpoint = _playerSpawnpoints[numPlayers - 1];
        GameObject instance = Instantiate(spawnpoint.Prefab.gameObject, spawnpoint.Spawnpoint.position, spawnpoint.Spawnpoint.rotation);
        NetworkServer.AddPlayerForConnection(conn, instance);
        _players[numPlayers - 1] = spawnpoint.Prefab;
        
        if (numPlayers == PlayerCount)
        {
            UIHandler.Instance.SetWaitingPlayerPanelState(false);
            _gameHandler.StartGame(_players);
        }
        else
        {
            UIHandler.Instance.SetWaitingPlayerPanelState(true);
        }
    }

    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        
        if (NetworkServer.connections.Count < PlayerCount)
        {
            UIHandler.Instance.SetWaitingPlayerPanelState(true);
        }
    }
}
