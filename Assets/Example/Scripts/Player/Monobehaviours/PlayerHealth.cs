using System;
using UnityEngine;

namespace Example.Scripts.Player.Monobehaviours
{
    public class PlayerHealth : MonoBehaviour
    {
        [SerializeField] private float currentHealth;
        [SerializeField] private float maxHealth;

        public static event Action<float>  OnHealthChanged;
    
        private void Start()
        {
            currentHealth = maxHealth;
        }

        public void AdjustHealth(float value)
        {
            currentHealth = Mathf.Clamp(currentHealth + value, 0, maxHealth);
            OnHealthChanged?.Invoke(currentHealth);
        }
    }
}
