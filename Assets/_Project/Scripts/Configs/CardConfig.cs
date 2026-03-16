using UnityEngine;

namespace _Project.Scripts.Configs
{
    public abstract class CardConfig : ScriptableObject
    {
        [SerializeField] private Sprite _icon;
        [SerializeField] private string _title;
        [SerializeField] private int _price;
        [SerializeField] private Color _titleColor = Color.white;
        
        public Sprite Icon => _icon;
        public string Title => _title;
        public int Price => _price;
        public Color TitleColor => _titleColor;
    }
}