using _Project.Scripts.Card;
using _Project.Scripts.City;
using _Project.Scripts.Enums;
using UnityEngine;

namespace _Project.Scripts.Handlers
{
    public class CardHandler : MonoBehaviour
    {
        [SerializeField] private CardContainer _cardContainer;
        
        private CardBuildGenerator _cardBuildGenerator;
        private GameHandler _gameHandler;
        private int _appliedCardCount;
        private bool _isBonusBuild;
        private bool _isMainBuildApplied;
        private int _ownerPlayerIndex;
        
        public static CardHandler Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public void Init(GameHandler handler)
        {
            _gameHandler = handler;
        }

        public void StartTurn(int ownerPlayerIndex, bool isBonusBuild)
        {
            _isMainBuildApplied = false;
            _isBonusBuild = isBonusBuild;
            _ownerPlayerIndex = ownerPlayerIndex;
    
            CreateCardBuild(_ownerPlayerIndex, false);
        }
        
        private void CreateCardBuild(int ownerPlayerIndex, bool isBonusBuild)
        {
            if (_cardBuildGenerator == null)
                _cardBuildGenerator = new CardBuildGenerator(_gameHandler.Config);

            _cardContainer.Fill(_cardBuildGenerator.Generate(isBonusBuild), ownerPlayerIndex, isBonusBuild);
            _appliedCardCount = 0; 
        }

        public bool TryApplyCard(CardConfig config, BuildPlace target, int ownerIndex)
        {
            if (config is DefenceCardConfig defenceConfig)
            {
                if (target.OwnerIndex != ownerIndex) return false;
                target.Build(defenceConfig);
                CardApplied();
                return true;
            }
            if (config is AttackCardConfig attackConfig)
            {
                if (target.OwnerIndex == ownerIndex) return false;

                if (attackConfig.Type == DamageType.Accurate)
                {
                    if (AttackUtils.TryApplyAccurateDamage(attackConfig.Damage, target, _gameHandler.Players[target.OwnerIndex]) == false)
                    {
                        return false;
                    }
                }
                else if (attackConfig.Type == DamageType.Area)
                {
                    if (AttackUtils.TryApplyAreaDamage(attackConfig.Damage, _gameHandler.Players[target.OwnerIndex]) == false)
                    {
                        return false;
                    }
                }
                else if (attackConfig.Type == DamageType.Layer)
                {
                    if (AttackUtils.TryApplyLayerDamage(attackConfig.Damage, target, _gameHandler.Players[target.OwnerIndex]) == false)
                    {
                        return false;
                    }
                }

                CardApplied();
                return true;
            }
            if (config is UpgradeCardConfig upgradeConfig)
            {
                if (target.OwnerIndex != ownerIndex) return false;
                CardApplied();
                return true;
            }

            return false;
        }

        public void VisualiseApplyCard(CardConfig config, BuildPlace target, int ownerIndex)
        {
            foreach (var player in _gameHandler.Players)
            {
                player.MainBuildPlace.SetOutlineState(false);
                
                foreach (var buildPlace in player.BuildPlaces)
                {
                    buildPlace.SetOutlineState(false);
                }
            }
            
            if(!target) return;
            if (config is DefenceCardConfig defenceConfig)
            {
                if (target.OwnerIndex != ownerIndex) return;
                target.SetOutlineState(true);
                return;
            }
            if (config is AttackCardConfig attackConfig)
            {
                if (target.OwnerIndex == ownerIndex) return;

                if (attackConfig.Type == DamageType.Accurate)
                {
                    AttackUtils.VisualiseAccurateDamage(target, _gameHandler.Players[target.OwnerIndex]);
                }
                else if (attackConfig.Type == DamageType.Area)
                {
                    AttackUtils.VisualiseAreaDamage(_gameHandler.Players[target.OwnerIndex]);
                }

                else if (attackConfig.Type == DamageType.Layer)
                {
                    AttackUtils.VisualiseLayerDamage(target, _gameHandler.Players[target.OwnerIndex]);
                }

                return;
            }
            if (config is UpgradeCardConfig upgradeConfig)
            {
                if (target.OwnerIndex != ownerIndex) return;
            }
        }

        private void CardApplied()
        {
            _appliedCardCount++;

            if (_isMainBuildApplied)
            {
                if (_gameHandler.Config.BonusCardApplyPerTurn <= _appliedCardCount)
                {
                    _cardContainer.Clear();
                    _gameHandler.NextTurn();
                }
            }
            else
            {
                if (_gameHandler.Config.CardApplyPerTurn <= _appliedCardCount)
                {
                    _isMainBuildApplied = true;
                
                    if (_isBonusBuild)
                    {
                        _cardContainer.Clear();
                        CreateCardBuild(_ownerPlayerIndex, true);
                    }
                    else
                    {
                        _cardContainer.Clear();
                        _gameHandler.NextTurn();
                    }
                }
            }
        }
    }
}