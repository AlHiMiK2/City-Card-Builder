using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Project.Scripts
{
    public class PlayerWalletView : MonoBehaviour
    {
        [SerializeField] private TMP_Text _moneyText;
        [SerializeField] private PlayerWallet _wallet;
        [Header("Effects")]
        [SerializeField] private float _punchStrenght;
        [SerializeField] private float _punchDuration;
        [SerializeField] private Ease _punchEase;
        
        private Tween _currentShakeTween;
        
        private void OnEnable()
        {
            _wallet.OnMoneyChange += MoneyChanged;
        }

        private void OnDisable()
        {
            _wallet.OnMoneyChange -= MoneyChanged;
        }

        private void MoneyChanged(int money)
        {
            _moneyText.text = money + "$";
            
            transform.DOKill();
        
            transform.DOPunchScale(Vector3.one * _punchStrenght, _punchDuration)
                .SetLoops(1, LoopType.Yoyo)
                .SetEase(_punchEase)
                .OnUpdate(() =>
                {
                    transform.localScale = Vector3.ClampMagnitude(transform.localScale, 2f);
                })
                .OnComplete(() => {
                    transform.localScale = Vector3.one;
                });
        }
    }
}