using System;
using System.Diagnostics;
using UnityEngine;
using Debug = UnityEngine.Debug;

namespace Example.Scripts.Player.Monobehaviours
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth;

        public float MaxHealth => maxHealth;
        public float CurrentHealth => currentHealth;

        public static event Action<float>  OnHealthChanged;
        public static event Action<float>  OnMaxHealthChanged;
    
        private void Start()
        {
            currentHealth = maxHealth;
            SetHealth(currentHealth);
            SetMaxHealth(maxHealth);
        }

        public void AdjustHealth(float value)
        {
            currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth);
        }

        public void SetHealth(float value)
        {
            currentHealth = Mathf.Clamp(value, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth);
        }
        
        public void SetMaxHealth(float value)
        {
            maxHealth = Mathf.Clamp(value, 0, float.MaxValue);
            currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
            OnMaxHealthChanged?.Invoke(maxHealth);
        }
        
    }
}
