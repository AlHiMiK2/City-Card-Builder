using UnityEngine;

namespace _Project.Scripts.Card
{
    public class CardConfig : ScriptableObject
    {
        [SerializeField] private string _label;
        [SerializeField] private Sprite _icon;
        
        public string Label => _label;
        public Sprite Icon => _icon;
    }
}
