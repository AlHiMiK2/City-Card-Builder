using _Project.Scripts.Handlers;
using _Project.Scripts.Interfaces;
using UnityEngine;
using UnityEngine.Events;

namespace _Project.Scripts
{
    public class PlayerWallet : MonoBehaviour, IWallet
    {
        private int _money;
        
        public event UnityAction<int> OnMoneyChange;

        private void Start()
        {
            _money = GameHandler.Instance.GameConfig.StartMoneyCount;
            OnMoneyChange?.Invoke(_money);
        }

        public bool TryTakeMoney(int money)
        {
            if (_money >= money)
            {
                _money -= money;
                OnMoneyChange?.Invoke(_money);
                return true;
            }
            
            return false;
        }

        public void AddMoney(int money)
        {
            _money += money;
            OnMoneyChange?.Invoke(_money);
        }
    }
}