using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private GameConfig _config;
        [SerializeField] private Player.Player[] _players;

        private CardHandler _cardHandler;
        private UIHandler _uiHandler;
        private int _turnOwnerIndex;

        public Player.Player[] Players => _players;
        public GameConfig Config => _config;
        public static GameHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _cardHandler = CardHandler.Instance;
            _cardHandler.Init(this);
            _uiHandler = UIHandler.Instance;
            _uiHandler.Init();
            
            for (var i = 0; i < _players.Length; i++)
            {
                _players[i].Init(_config.WalletCapacity, i);
            }
            
            StartTurn();
        }

        private void StartTurn()
        {
            bool isBonusBuild = _players[_turnOwnerIndex].Wallet.IsFulled;
            
            _cardHandler.CreateCardBuild(_turnOwnerIndex, isBonusBuild);
            
            if (isBonusBuild)
            {
                _players[_turnOwnerIndex].Wallet.ClearScore();
            }
            
            _uiHandler.SetTurnViewValue(_turnOwnerIndex + 1);
        }

        public void NextTurn()
        {
            _turnOwnerIndex++;
            
            if (_turnOwnerIndex > _players.Length - 1)
            {
                _turnOwnerIndex = 0;
                EndRound();
            }
            
            StartTurn();
        }

        private void EndRound()
        {
            foreach (var player in _players)
            {
                player.Earn();
            }
        }
    }
}