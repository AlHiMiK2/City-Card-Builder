using _Project.Scripts.City;
using _Project.Scripts.City.Wallet;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private BuildPlace[] _buildPlaces;
        [SerializeField] private MainBuildPlace _mainBuildPlace;
        [SerializeField] private WalletView _walletView;
        
        private CardHandler _cardHandler;
        private GameHandler _gameHandler;
        private Wallet _wallet;
        private int _index;
        private bool _isDead;

        public Wallet Wallet => _wallet;
        public BuildPlace[] BuildPlaces => _buildPlaces;
        public MainBuildPlace MainBuildPlace => _mainBuildPlace;
        public bool IsDead => _isDead;

        public void Init(int walletCapacity, int index)
        {
            _wallet = new Wallet(walletCapacity);
            _walletView.Init(_wallet);
            _wallet.AddScore(0);
            _index = index;
            
            int placeIndex = 0;
            foreach (var place in _buildPlaces)
            {
                place.Init(placeIndex, _index, GameHandler.Instance.Config.CardDatabase.DefaultDefenceCardConfig);
                placeIndex++;
            }
            
            _mainBuildPlace.Init(placeIndex, _index, GameHandler.Instance.Config.CardDatabase.MainDefenceCardConfig);
            _mainBuildPlace.ConstructionData.HealthChanged += OnMainPlaceHealthChanged;
        }        
        
        private void OnDestroy()
        {
            _mainBuildPlace.ConstructionData.HealthChanged -= OnMainPlaceHealthChanged;
        }
        
        public void Earn()
        {
            int earn = 0;
            
            foreach (var place in _buildPlaces)
            {
                earn += place.ConstructionData.Earn;
            }
            
            _wallet.AddScore(earn);
        }

        private void OnMainPlaceHealthChanged()
        {
            if (_mainBuildPlace.ConstructionData.Health <= 0)
            {
                _isDead = true;
            }
        }

        private void OnValidate()
        {
            if (_buildPlaces.Length != 6)
            {
                _buildPlaces = new BuildPlace[6];
            }
        }
    }
}