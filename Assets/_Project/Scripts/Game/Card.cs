using _Project.Scripts.Configs;
using _Project.Scripts.UI;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Project.Scripts
{
    [RequireComponent(typeof(Draggable))]
    public class Card : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TMP_Text _titleText;
        [SerializeField] private TMP_Text _priceText;
        
        private CardConfig _config;
        private Draggable _draggable;
        private BasePlayer _player;

        public event UnityAction OnUsed;
        public CardConfig Config => _config;
        public Draggable Draggable => _draggable;

        private void Awake()
        {
            _draggable = GetComponent<Draggable>();
        }

        public void Init(CardConfig config, BasePlayer player)
        {
            _config = config;
            _icon.sprite = _config.Icon;
            _icon.SetNativeSize();
            _titleText.text = _config.Title;
            _titleText.color = _config.TitleColor;
            _priceText.text = _config.Price + "$";
            _player = player;
        }

        public bool TryUse()
        {
            return _player.Wallet.TryTakeMoney(_config.Price);
        }

        public void Used()
        {
            _draggable.Container.RemoveItem(_draggable.ID);
            OnUsed?.Invoke();
        }
    }
}