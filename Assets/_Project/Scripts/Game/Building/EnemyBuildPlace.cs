using _Project.Scripts.Configs;
using _Project.Scripts.Game.Building;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    public class EnemyBuildPlace : BaseBuildPlace, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (this is EnemyMainPlace mainPlace)
            {
                mainPlace.OnDrop(eventData);
                return;
            }
            if(eventData.pointerDrag == null) return;
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