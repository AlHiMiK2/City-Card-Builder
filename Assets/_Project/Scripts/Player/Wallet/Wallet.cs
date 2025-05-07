using UniRx;
using UnityEngine.Events;

namespace _Project.Scripts.City.Wallet
{
    public class Wallet
    {
        private ReactiveProperty<int> _money;
        private int _capacity;
        
        public IReadOnlyReactiveProperty<int> Money => _money;
        public event UnityAction Fulled;
        
        public Wallet(int capacity)
        {
            _money = new (0);
            _capacity = capacity;
        }

        public void AddMoney(int money)
        {
            int sum = _money.Value + money;
            

            if (sum >= _capacity)
            {
                sum = _capacity;
                _money.SetValueAndForceNotify(sum);
                Fulled?.Invoke();
            }
        }
        
        public void ResetMoney(int money)
        {
            _money.SetValueAndForceNotify(0);
        }
    }
}