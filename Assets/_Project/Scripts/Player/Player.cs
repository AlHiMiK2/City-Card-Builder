using System;
using System.Linq;
using _Project.Scripts.City;
using _Project.Scripts.City.Wallet;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private BuildLine[] _buildLines;
        [SerializeField] private WalletView _walletView;
        
        private CardHandler _cardHandler;
        private GameHandler _gameHandler;
        private Wallet _wallet;
        private int _index;
        private bool _isDead;

        public Wallet Wallet => _wallet;
        public BuildLine[] BuildLines => _buildLines;
        public BuildPlace MainBuildPlace => _buildLines.Last().Places[0];
        public bool IsDead => _isDead;
        
        [Serializable]
        public class BuildLine
        {
            public BuildPlace[] Places;
        }

        public void Init(int walletCapacity, int index)
        {
            _wallet = new Wallet(walletCapacity);
            _walletView.Init(_wallet);
            _wallet.AddScore(0);
            _index = index;
            
            int placeIndex = 0;
            foreach (var line in _buildLines)
            {
                foreach (var place in line.Places)
                {
                    if (place as MainBuildPlace)
                    {
                        place.Init(placeIndex, _index, GameHandler.Instance.Config.CardDatabase.MainDefenceCardConfig);
                    }
                    else
                    {
                        place.Init(placeIndex, _index, GameHandler.Instance.Config.CardDatabase.DefaultDefenceCardConfig);
                    }
                    
                    placeIndex++;
                }
            }
            
            MainBuildPlace.ConstructionData.HealthChanged += OnMainPlaceHealthChanged;
        }        
        
        private void OnDestroy()
        {
            MainBuildPlace.ConstructionData.HealthChanged -= OnMainPlaceHealthChanged;
        }
        
        public void Earn()
        {
            int earn = 0;
            
            foreach (var line in _buildLines)
            {
                foreach (var place in line.Places)
                {
                    earn += place.ConstructionData.Earn;
                }
            }
            
            _wallet.AddScore(earn);
        }

        private void OnMainPlaceHealthChanged()
        {
            if (MainBuildPlace.ConstructionData.Health <= 0)
            {
                _isDead = true;
            }
        }
    }
}