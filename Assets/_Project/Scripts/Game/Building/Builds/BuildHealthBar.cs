using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts
{
    public class BuildHealthBar : MonoBehaviour
    {
        [SerializeField] private Slider _slider;
        [SerializeField] private Build _build;
        [SerializeField] private float _duration;
        [SerializeField] private Ease _ease;

        private void OnEnable()
        {
            _build.OnHealthChange += HealthChanged;
        }

        private void OnDisable()
        {
            _build.OnHealthChange -= HealthChanged;
        }

        private void HealthChanged(int health, int maxHealth)
        {
            _slider.DOValue(health / (float)maxHealth, 0.2f).SetEase(_ease);
        }
    }
}