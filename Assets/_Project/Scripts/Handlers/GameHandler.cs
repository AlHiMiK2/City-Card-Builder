using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private GameConfig _config;
        [SerializeField] private Player[] _players;

        private CardHandler _cardHandler;
        private UIHandler _uiHandler;
        private int _turnOwnerIndex;
        private bool _isEnd;

        public Player[] Players => _players;
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
            bool isWalletFulled = _players[_turnOwnerIndex].Wallet.IsFulled;
            
            _cardHandler.StartTurn(_turnOwnerIndex, isWalletFulled);
            
            if (isWalletFulled)
            {
                _players[_turnOwnerIndex].Wallet.ClearScore();
            }
            
            _uiHandler.SetTurnViewValue(_turnOwnerIndex + 1);
        }

        public void NextTurn()
        {
            if (TryEndGame(out int winPlayerIndex))
            {
                Debug.Log("Win Player: " + winPlayerIndex + 1);
            }
            else
            {
                _turnOwnerIndex++;
            
                if (_turnOwnerIndex > _players.Length - 1)
                {
                    _turnOwnerIndex = 0;
                    EndRound();
                }
            
                StartTurn();
            }
        }

        private void EndRound()
        {
            foreach (var player in _players)
            {
                player.Earn();
            }
        }

        private bool TryEndGame(out int winPlayerIndex)
        {
            int lifePlayerCount = 0;
            winPlayerIndex = 0;
            
            for (var i = 0; i < _players.Length; i++)
            {
                if (!_players[i].IsDead)
                {
                    lifePlayerCount++;
                    winPlayerIndex = i;
                }
            }

            bool isEnd = lifePlayerCount <= 1;
            
            return isEnd;
        }
    }
}