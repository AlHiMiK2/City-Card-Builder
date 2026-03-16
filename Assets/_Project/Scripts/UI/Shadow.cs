using UnityEngine;

namespace _Project.Scripts.UI
{
    public class Shadow : MonoBehaviour
    {
        [SerializeField] private float ShadowDistance = 0.02f;
        
        private Transform _parent;
        
        private void Awake()
        {
            _parent = transform.parent;
        }

        private void Update()
        {
            Vector3 pos = _parent.position + ShadowDistance * Vector3.down;
            transform.SetPositionAndRotation(pos, _parent.rotation);
        }
    }
}