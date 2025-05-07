using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private GameConfig _config;
        [SerializeField] private Player.Player[] _players;

        private CardHandler _cardHandler;
        private int _turnOwnerIndex;
        
        public GameConfig Config => _config;
        public static GameHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _cardHandler = CardHandler.Instance;
            
            for (var i = 0; i < _players.Length; i++)
            {
                _players[i].Init(_config.WalletCapacity, i);
            }
            
            StartTurn();
        }

        private void StartTurn()
        {
            _cardHandler.CreateCardBuild(_turnOwnerIndex);
        }

        public void NextTurn()
        {
            _turnOwnerIndex++;
            
            if (_turnOwnerIndex > _players.Length - 1)
            {
                _turnOwnerIndex = 0;
            }
            
            StartTurn();
        }
    }
}