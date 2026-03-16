using System;
using _Project.Scripts.Handlers;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts
{
    public class EnemyWallet : MonoBehaviour, IWallet
    {
        private int _money;
        
        public int Money => _money;

        private void Start()
        {
            _money = GameHandler.Instance.GameConfig.StartMoneyCount;
        }

        public bool TryTakeMoney(int money)
        {
            if (_money >= money)
            {
                _money -= money;
                return true;
            }
            
            return false;
        }

        public void AddMoney(int money)
        {
            _money += money;
        }
    }
}