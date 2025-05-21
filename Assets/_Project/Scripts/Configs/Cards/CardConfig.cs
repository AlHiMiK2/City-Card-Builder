using UnityEngine;

namespace _Project.Scripts.Card
{
    public class CardConfig : ScriptableObject
    {
        [SerializeField] private string _label;
        [SerializeField] private Sprite _icon;
        [SerializeField] private int _id;
        
        public string Label => _label;
        public Sprite Icon => _icon;
        public int Id => _id;

        public void SetId(int id)
        {
            _id = id;
        }
    }
}
