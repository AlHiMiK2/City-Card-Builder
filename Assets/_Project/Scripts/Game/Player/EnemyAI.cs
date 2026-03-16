using System.Collections.Generic;
using System.Linq;
using _Project.Scripts.Configs;
using _Project.Scripts.Game;
using _Project.Scripts.Handlers;
using UnityEngine;

namespace _Project.Scripts
{
    [RequireComponent(typeof(EnemyWallet))]
    public class EnemyAI : BasePlayer
    {
        [SerializeField] private EnemyCardContainer _cardContainer;

        private float _logicRate = 1f;
        private float _logicTime;
        
        private void Awake()
        {
            Wallet = GetComponent<EnemyWallet>();
        }

        private void Update()
        {
            if (_logicTime + _logicRate <= Time.time)
            {
                Logic();
                _logicTime = Time.time;
            }
        }

        private void Logic()
        {
            foreach (var config in _cardContainer.CardConfigs)
            {
                if (config is BuildCardConfig buildConfig)
                {
                    if (Wallet.TryTakeMoney(config.Price))
                    {
                        BuildPlaces[Random.Range(0, BuildPlaces.Length)].Build(buildConfig);
                        _cardContainer.RemoveConfig(config);
                        break;
                    }
                }
                else if (config is AttackCardConfig attackConfig)
                {
                    if (Wallet.TryTakeMoney(config.Price))
                    {
                        Player player = GameHandler.Instance.Player;
                        List<BaseBuildPlace> places = player.BuildPlaces.ToList();
                        places.Add(player.MainPlace);
                        places[Random.Range(0, places.Count)].Attack(attackConfig.Damage);
                        _cardContainer.RemoveConfig(config);
                        break;
                    }
                }
            }
        }
    }
}