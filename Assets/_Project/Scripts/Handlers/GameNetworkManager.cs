using _Project.Scripts.Handlers;
using Mirror;
using UnityEngine;

public class GameNetworkManager : NetworkManager
{
    [SerializeField] private GameHandler _gameHandler;
    
    public const int PlayerCount = 2;

    public override void OnStartServer()
    {
        base.OnStartServer();
        UIHandler.Instance.SetWaitingPlayerPanelState(true);
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        
        if (NetworkServer.connections.Count >= PlayerCount)
        {
            UIHandler.Instance.SetWaitingPlayerPanelState(false);
            _gameHandler.StartGame();
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
