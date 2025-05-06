using _Project.Scripts.City;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(fileName = "New Game Config", menuName = "Create Game Config", order = 0)]
    public class GameConfig : ScriptableObject
    {
        [SerializeField] private CityResources _startResources;
        [SerializeField] private int _maxCards;

        public CityResources StartResources => _startResources;
        public int MaxCards => _maxCards;
    }
}