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
        
        private UIHandler _uiHandler;
        private Wallet _wallet;
        private bool _isTurn;
        private bool _isInit;
        
        public Wallet Wallet => _wallet;
        
        [Serializable]
        private class BuildPlaceLine
        {
            public BuildPlace[] Places;
        }

        public void Init(int walletCapacity)
        {
            _wallet = new Wallet(walletCapacity);
            _uiHandler = UIHandler.Instance;
        }

        public void EnableTurn()
        {
            _uiHandler.CardContainer.Fill();

            foreach (var lines in _buildPlaceLines)
            {
                foreach (var place in lines.Places)
                {
                    place.Enable();
                }
            }
        }

        public void DisableTurn()
        {
            _uiHandler.CardContainer.Clear();
            
            foreach (var lines in _buildPlaceLines)
            {
                foreach (var place in lines.Places)
                {
                    place.Disable();
                }
            }
        }
    }
}