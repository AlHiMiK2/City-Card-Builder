using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private City.City _city;

        public City.City City => _city;
        
        public static GameHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}