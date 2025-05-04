using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Card
{
    public class Card : Draggable
    {
        [SerializeField] private TMP_Text _labelView;
        [SerializeField] private Image _icon;
        
        private CardConfig _config;

        public void Init(CardConfig config)
        {
            _config = config;
            _labelView.text = _config.Label;
            _icon.sprite = _config.Icon;
        }
    }
}