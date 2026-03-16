using _Project.Scripts.Configs;
using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class GameHandler : MonoBehaviour
    {
        [SerializeField] private Player _player;
        [SerializeField] private GameConfig _gameConfig;

        public Player Player => _player;
        public GameConfig GameConfig => _gameConfig;
        
        public static GameHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void PlayerDead()
        {
            Debug.Log("Player Dead");
        }

        public void EnemyDead()
        {
            Debug.Log("Enemy Dead");
        }
    }
}