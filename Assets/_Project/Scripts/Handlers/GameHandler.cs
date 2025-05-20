using System;
using Mirror;
using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class GameHandler : NetworkBehaviour
    {
        [SerializeField] private GameConfig _config;
        [SerializeField] private PlayerSpawnpoint[] _playerSpawnpoints;

        private bool _isStarted;
        private CardHandler _cardHandler;
        private UIHandler _uiHandler;
        private int _turnOwnerIndex;
        private bool _isEnd;
        private Player[] _players;

        public Player[] Players => _players;
        public GameConfig Config => _config;
        public static GameHandler Instance { get; private set; }

        [Serializable]
        private class PlayerSpawnpoint
        {
            public Player Prefab;
            public Transform Spawnpoint;
        }
        
        private void Awake()
        {
            Instance = this;
        }

        [Server]
        public void StartGame()
        {
            if(_isStarted) return;
            _isStarted = true;
            _cardHandler = CardHandler.Instance;
            _cardHandler.Init(this);
            _uiHandler = UIHandler.Instance;
            _uiHandler.Init();
            _players = new Player[_playerSpawnpoints.Length];
            
            for (var i = 0; i < _players.Length; i++)
            {
                var spawnpoint = _playerSpawnpoints[i];
                GameObject instance = Instantiate(spawnpoint.Prefab.gameObject, spawnpoint.Spawnpoint.position, spawnpoint.Spawnpoint.rotation);
                NetworkServer.AddPlayerForConnection(NetworkServer.connections[i], instance);
                _players[i] = instance.GetComponent<Player>();
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
                _cardHandler.ClearBuild();
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

        protected override void OnValidate()
        {
            base.OnValidate();
            
            if (_playerSpawnpoints.Length != GameNetworkManager.PlayerCount)
            {
                _playerSpawnpoints = new PlayerSpawnpoint[GameNetworkManager.PlayerCount];
            }
        }
    }
}