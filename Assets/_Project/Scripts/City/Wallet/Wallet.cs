using UniRx;

namespace _Project.Scripts.City.Wallet
{
    public class Wallet
    {
        private ReactiveProperty<int> _wood;
        private ReactiveProperty<int> _stone;
        private ReactiveProperty<int> _metal;
        
        public IReadOnlyReactiveProperty<int> Wood => _wood;
        public IReadOnlyReactiveProperty<int> Stone => _stone;
        public IReadOnlyReactiveProperty<int> Metal => _metal;

        public Wallet(int wood, int stone, int metal)
        {
            _wood = new (wood);
            _stone = new (stone);
            _metal = new (metal);
        }
    }
}