using UnityEngine;

namespace _Project.Scripts.City
{
    public class City : MonoBehaviour
    {
        private Wallet.Wallet _wallet;

        public Wallet.Wallet Wallet => _wallet;

        private void Awake()
        {
            _wallet = new Wallet.Wallet(0, 0, 0);
        }
    }
}