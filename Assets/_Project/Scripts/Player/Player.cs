using _Project.Scripts.City;
using _Project.Scripts.City.Wallet;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private BuildPlace[] _buildPlaces;
        [SerializeField] private BuildPlace _mainBuildPlace;
        [SerializeField] private WalletView _walletView;
        
        private CardHandler _cardHandler;
        private GameHandler _gameHandler;
        private Wallet _wallet;
        private int _index;

        public Wallet Wallet => _wallet;
        public BuildPlace[] BuildPlaces => _buildPlaces;
        public BuildPlace MainBuildPlace => _mainBuildPlace;

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

        private void OnValidate()
        {
            if (_buildPlaces.Length != 6)
            {
                _buildPlaces = new BuildPlace[6];
            }
        }
    }
}