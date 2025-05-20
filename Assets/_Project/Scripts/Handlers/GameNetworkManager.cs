using System;
using _Project.Scripts;
using _Project.Scripts.Handlers;
using Mirror;
using UnityEngine;

public class GameNetworkManager : NetworkManager
{
    [Header("Custom")]
    [SerializeField] private GameHandler _gameHandler;
    [SerializeField] private PlayerSpawnpoint[] _spawnpoints;

    private Player[] _players;
    
    public const int PlayerCount = 2;

    [Serializable]
    public struct PlayerSpawnpoint
    {
        public Player Prefab;
        public Transform Target;
    }
    
    public struct SpawnMessage : NetworkMessage
    {
        public PlayerSpawnpoint Spawnpoint;
    }
    
    public override void OnStartServer()
    {
        base.OnStartServer();
        _players = new Player[PlayerCount];
        UIHandler.Instance.SetWaitingPlayerPanelState(true);
        NetworkServer.RegisterHandler<SpawnMessage>(OnCreatePlayer);
    }

    private void OnCreatePlayer(NetworkConnectionToClient conn, SpawnMessage message)
    {
        Player instance = Instantiate(message.Spawnpoint.Prefab, message.Spawnpoint.Target.position, message.Spawnpoint.Target.rotation);
        _players[NetworkServer.connections.Count - 1] = instance;
        NetworkServer.AddPlayerForConnection(conn, instance.gameObject);
        TryStartGame();
    }

    private void TryStartGame()
    {
        if (NetworkServer.connections.Count == PlayerCount)
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

    public override void OnClientConnect()
    {
        base.OnClientConnect();
        SpawnMessage message = new SpawnMessage() {Spawnpoint = _spawnpoints[NetworkServer.connections.Count - 1]};
        NetworkClient.Send(message);
    }
}
