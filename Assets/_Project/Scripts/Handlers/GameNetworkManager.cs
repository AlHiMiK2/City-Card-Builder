using _Project.Scripts.Handlers;
using Mirror;

public class GameNetworkManager : NetworkManager
{
    private const int MinPlayerCount = 2;

    public override void OnStartServer()
    {
        base.OnStartServer();
        UIHandler.Instance.SetWaitingPlayerPanelState(true);
    }

    public override void OnServerAddPlayer(NetworkConnectionToClient conn)
    {
        base.OnServerAddPlayer(conn);
        
        if (numPlayers >= MinPlayerCount)
        {
            UIHandler.Instance.SetWaitingPlayerPanelState(false);
        }
        else
        {
            UIHandler.Instance.SetWaitingPlayerPanelState(true);
        }
    }
    
    public override void OnServerDisconnect(NetworkConnectionToClient conn)
    {
        base.OnServerDisconnect(conn);
        
        if (numPlayers < MinPlayerCount)
        {
            UIHandler.Instance.SetWaitingPlayerPanelState(true);
        }
    }
}
