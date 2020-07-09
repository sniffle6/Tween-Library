using System.Collections.Generic;
using Example.Scripts.Player.Monobehaviours;
using Tween_Library.Scripts;
using Tween_Library.Scripts.Effects;
using UnityEngine;
using UnityEngine.UI;

namespace Example.Scripts.UI.Monobehaviours
{
    public class HealthPanel : MonoBehaviour
    {
        [SerializeField] private Slider slider;
        [SerializeField] private float waitTime;

        [Header("Scale Settings")]
        [SerializeField] private Vector3 maxScaleSize;
        [SerializeField] private float scaleSpeed;
        
        [Header("Shake Settings")]
        [SerializeField] private float maxShakeRotation;
        [SerializeField] private float shakeSpeed;

        [Header("Color Settings")]
        [SerializeField] private Color takeDamageColor;
        [SerializeField] private Image sliderFill;

        private EffectBuilder _takeDamageEffect;
        private  WaitForSeconds _wait; 
        
        private void Awake()
        {
            _wait = new WaitForSeconds(waitTime);
            _takeDamageEffect = new EffectBuilder(this);
            _takeDamageEffect
                .AddEffect(new ShakeRectEffect(slider.GetComponent<RectTransform>(), maxShakeRotation, shakeSpeed, OnEffectComplete))
                .AddEffect(new FlashColorEffect(sliderFill.color, takeDamageColor, sliderFill, _wait))
                .AddEffect(new ScaleRectEffect(slider.GetComponent<RectTransform>(), maxScaleSize, scaleSpeed, _wait))
                .OnAllEffectsComplete += OnAllEffectsComplete;
        }

        private void OnEnable()
        {
            PlayerHealth.OnHealthChanged += HandleHealthChanged;
            PlayerHealth.OnMaxHealthChanged += HandleMaxHealthChanged;
        }

        private void OnDestroy()
        {
            PlayerHealth.OnHealthChanged -= HandleHealthChanged;
            PlayerHealth.OnMaxHealthChanged -= HandleMaxHealthChanged;
        }


        private void HandleHealthChanged(float currentHealth)
        {
            slider.value = currentHealth;
            _takeDamageEffect.ExecuteEffects();
        }
        
        private void HandleMaxHealthChanged(float maxHealth)
        {
            slider.maxValue = maxHealth;
        }

        private void OnEffectComplete(IUiEffect effect)
        {
            print($"Completed {effect}!");
        }

        private void OnAllEffectsComplete()
        {
            print("Completed all effects!");
        }

    }
}