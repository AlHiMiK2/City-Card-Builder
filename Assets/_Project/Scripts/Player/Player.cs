using System;
using _Project.Scripts.City;
using _Project.Scripts.City.Wallet;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private BuildPlaceLine[] _buildPlaceLines;
        [SerializeField] private WalletView _walletView;
        
        private CardHandler _cardHandler;
        private GameHandler _gameHandler;
        private Wallet _wallet;
        private int _index;

        public Wallet Wallet => _wallet;
        
        [Serializable]
        private class BuildPlaceLine
        {
            public BuildPlace[] Places;
        }

        public void Init(int walletCapacity, int index)
        {
            _wallet = new Wallet(walletCapacity);
            _walletView.Init(_wallet);
            _wallet.AddScore(0);
            _index = index;

            foreach (var line in _buildPlaceLines)
            {
                foreach (var place in line.Places)
                {
                    place.Init(_index, GameHandler.Instance.Config.CardDatabase.DefaultDefenceCardConfig);
                }
            }
        }
        
        public void Earn()
        {
            int earn = 0;
            
            foreach (var line in _buildPlaceLines)
            {
                foreach (var place in line.Places)
                {
                    earn += place.ConstructionData.Earn;
                }
            }
            
            _wallet.AddScore(earn);
        }
    }
}