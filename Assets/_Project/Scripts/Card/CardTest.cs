using UnityEngine;

namespace _Project.Scripts.Card
{
    public class CardTest : MonoBehaviour
    {
        [SerializeField] private Card _prefab;
        [SerializeField] private CardConfig[] _configs;

        private void Start()
        {
            for (int i = 0; i < 5; i++)
            {
                var instance = Instantiate(_prefab, transform);
                instance.Init(_configs[Random.Range(0, _configs.Length)]);
            }
        }
    }
}