using _Project.Scripts.City;
using _Project.Scripts.City.Wallet;
using UnityEngine;

namespace _Project.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        private Wallet _wallet;
        private bool _isTurn;
        private bool _isInit;
        
        public Wallet Wallet => _wallet;

        public void Init(CityResources startResources)
        {
            _wallet = new Wallet(startResources);
        }
    }
}