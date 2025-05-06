using UniRx;

namespace _Project.Scripts.City.Wallet
{
    public class Wallet
    {
        private ReactiveProperty<CityResources> _resources;
        
        public IReadOnlyReactiveProperty<CityResources> Resources => _resources;
        
        public Wallet(CityResources resources)
        {
            _resources = new (resources);
        }

        public void AddResources(CityResources resources)
        {
            _resources.SetValueAndForceNotify(_resources.Value + resources);
        }
        
        public bool TryTakeResources(CityResources resources)
        {
            var resourcesAfterTake = _resources.Value - resources;
            bool isWoodValid = resourcesAfterTake.Wood >= 0;
            bool isStoneValid = resourcesAfterTake.Stone >= 0;
            bool isMetalValid = resourcesAfterTake.Metal >= 0;

            if (isWoodValid && isStoneValid && isMetalValid)
            {
                _resources.SetValueAndForceNotify(resourcesAfterTake);
                return true;
            }

            return false;
        }
    }
}