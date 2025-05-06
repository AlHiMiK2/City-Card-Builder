using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private GameConfig _config;
        [SerializeField] private Player.Player[] _players;

        private int _currentTurn;
        
        public static GameHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            
            foreach(var player in _players)
            {
                player.Init(_config.StartResources);
            }
        }
    }
}