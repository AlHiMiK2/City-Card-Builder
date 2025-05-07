using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class GameHandler : StateMachine
    {
        [SerializeField] private GameConfig _config;
        [SerializeField] private Player.Player[] _players;

        public static GameHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
            
            foreach(var player in _players)
            {
                player.Init(_config.WalletCapacity);
            }
        }
    }
}