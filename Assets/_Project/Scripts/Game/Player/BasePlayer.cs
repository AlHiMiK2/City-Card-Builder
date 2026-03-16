using _Project.Scripts.Game.Building;
using _Project.Scripts.Handlers;
using _Project.Scripts.Interfaces;
using UnityEngine;

namespace _Project.Scripts
{
    public abstract class BasePlayer : MonoBehaviour
    {
        [SerializeField] private BaseBuildPlace[] buildPlaces;
        [SerializeField] private BaseBuildPlace _mainPlace;

        public BaseBuildPlace[] BuildPlaces => buildPlaces;
        public BaseBuildPlace MainPlace => _mainPlace;
        
        public IWallet Wallet {get; protected set;}
        
        protected void Start()
        {
            _mainPlace.Init(this);
            
            foreach (var buildPlace in BuildPlaces)
            {
                buildPlace.Init(this);
            }

            _mainPlace.Build(GameHandler.Instance.GameConfig.MainBuild);
        }
    }
}