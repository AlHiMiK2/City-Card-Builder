using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class UIHandler : MonoBehaviour
    {
        [SerializeField] private Canvas _canvas;
        [SerializeField] private TMP_Text _constructionDataViewPrefab;
        [SerializeField] private string _turnPrefix;
        [SerializeField] private string _turnPostfix;
        [SerializeField] private TMP_Text _turnView;
        [SerializeField] private GameObject _waitingPlayerPanel;

        private List<List<TMP_Text>> _constructionDataViews = new ();
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
        
        public void AddConstructionDataView(Vector3 worldPosition, int ownerIndex)
        {
            if (_constructionDataViews.Count - 1 < ownerIndex)
            {
                _constructionDataViews.Add(new List<TMP_Text>());
            }
            
            Vector3 position = _camera.WorldToScreenPoint(worldPosition);
            var instance = Instantiate(_constructionDataViewPrefab, position, Quaternion.identity, transform);
            _constructionDataViews[ownerIndex].Add(instance);
        }

        public void SetConstructionDataViewValue(int maxHealth, int health, int earn, int index, int ownerIndex)
        {
            if (health > 0)
            {
                _constructionDataViews[ownerIndex][index].text = $"{health}/{maxHealth}HP \n {earn} Earn";
            }
            else
            {
                _constructionDataViews[ownerIndex][index].text = "";
            }
        }

        public void SetTurnViewValue(int turn)
        {
            _turnView.text = _turnPrefix + turn + _turnPostfix;
        }

        public void SetWaitingPlayerPanelState(bool state)
        {
            _waitingPlayerPanel.SetActive(state);
            _turnView.gameObject.SetActive(!state);
        }
    }
}