using _Project.Scripts.Configs;
using UnityEngine.EventSystems;

namespace _Project.Scripts.Game.Building
{
    public class EnemyMainPlace : EnemyBuildPlace
    {
        public new void OnDrop(PointerEventData eventData)
        {
            if (eventData.pointerDrag == null) return;
            if (!eventData.pointerDrag.TryGetComponent(out Card card)) return;
            if (card.Config is AttackCardConfig config)
            {
                if (card.TryUse())
                {
                    Attack(config.Damage);
                    card.Used();
                }
            }
        }
    }
}