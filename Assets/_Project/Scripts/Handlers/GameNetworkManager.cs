using _Project.Scripts.Handlers;
using Mirror;
using UnityEngine;

public class GameNetworkManager : NetworkManager
{
    private const int MinPlayerCount = 2;

    public override void OnStartServer()
    {
        base.OnStartServer();
        UIHandler.Instance.SetWaitingPlayerPanelState(true);
    }

    public override void OnServerConnect(NetworkConnectionToClient conn)
    {
        base.OnServerConnect(conn);
        
        Debug.Log("Connected");
        if (NetworkServer.connections.Count >= MinPlayerCount)
        {
            UIHandler.Instance.SetWaitingPlayerPanelState(false);
            GameHandler.Instance.StartGame();
        }
        else
        {
            UIHandler.Instance.SetWaitingPlayerPanelState(true);
        }
    }
    
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        
        if (NetworkServer.connections.Count < MinPlayerCount)
        {
            UIHandler.Instance.SetWaitingPlayerPanelState(true);
        }
    }
}
