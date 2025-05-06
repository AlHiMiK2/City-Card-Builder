using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;

        public Canvas Canvas => _canvas;
        
        public static UIHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}