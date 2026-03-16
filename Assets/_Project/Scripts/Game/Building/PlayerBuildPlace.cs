using _Project.Scripts.Configs;
using _Project.Scripts.Game.Building;
using UnityEngine.EventSystems;

namespace _Project.Scripts
{
    public class PlayerBuildPlace : BaseBuildPlace, IDropHandler
    {
        public void OnDrop(PointerEventData eventData)
        {
            if (this is PlayerMainPlace mainPlace)
            {
                mainPlace.OnDrop(eventData);
                return;
            }
            if (eventData.pointerDrag == null) return;
            if (!eventData.pointerDrag.TryGetComponent(out Card card)) return;
            if (card.Config is BuildCardConfig config)
            {
                if (card.TryUse())
                {
                    Build(config);
                    card.Used();
                }
            }
        }
    }
}