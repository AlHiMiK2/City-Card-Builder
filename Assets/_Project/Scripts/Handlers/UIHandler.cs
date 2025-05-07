using _Project.Scripts.Card;
using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private CardContainer _cardContainer;
        
        public CardContainer CardContainer => _cardContainer;
        public Canvas Canvas => _canvas;
        
        public static UIHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
    }
}