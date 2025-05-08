using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _healthViewPrefab;
        
        private List<List<TMP_Text>> _healthBars = new ();
        private Camera _camera;
        
        public Canvas Canvas => _canvas;
        
        public static UIHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Init()
        {
            _camera = Camera.main;
        }
        
        public void AddHealthBar(Vector3 worldPosition, int ownerIndex)
        {
            if (_healthBars.Count - 1 < ownerIndex)
            {
                _healthBars.Add(new List<TMP_Text>());
            }
            
            Vector3 position = _camera.WorldToScreenPoint(worldPosition);
            var instance = Instantiate(_healthViewPrefab, position, quaternion.identity, transform);
            _healthBars[ownerIndex].Add(instance);
        }

        public void SetHealthViewValue(int maxHealth, int health, int index, int ownerIndex)
        {
            if (health > 0)
            {
                _healthBars[ownerIndex][index].text = $"{health}/{maxHealth}HP";
            }
            else
            {
                _healthBars[ownerIndex][index].text = $"";
            }
        }
    }
}