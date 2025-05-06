using UnityEngine;

namespace _Project.Scripts.City
{
    public abstract class BuildPlace : MonoBehaviour
    {
        [SerializeField] private bool _isActive;

        public bool TryBuild(Construction prefab)
        {
            if(_isActive)
                Instantiate(prefab, transform.position + Vector3.up, Quaternion.identity);
            return _isActive;
        }
    }
}