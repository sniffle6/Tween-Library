using System;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Example.Scripts.Player.Monobehaviours.Editor
{
    [CustomEditor(typeof(PlayerHealth))]
    public class HealthEditor : UnityEditor.Editor
    {
        private PlayerHealth _health;
        private VisualElement _rootElement;
        private VisualTreeAsset _visualTree;
        
        
        private void OnEnable()
        {
            _health = target as PlayerHealth;
            _rootElement = new VisualElement();
            
            _visualTree = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>("Assets/PlayerHealthI.uxml");
            var uss = AssetDatabase.LoadAssetAtPath<StyleSheet>("Assets/PlayerHealthI.uss");
            
            _rootElement.styleSheets.Add(uss);
        }

        public override VisualElement CreateInspectorGUI()
        {
            var root = _rootElement;
            root.Clear();

            _visualTree.CloneTree(root);

            var currentHealth = root.Q<FloatField>("CurrentHealth");
            var maxHealth = root.Q<FloatField>("MaxHealth");
            var healthBar = root.Q<ProgressBar>("HealthBar");
            
            healthBar.value = _health.MaxHealth / _health.CurrentHealth;
            
            currentHealth.BindProperty(serializedObject.FindProperty("currentHealth"));
            currentHealth.RegisterValueChangedCallback(evt =>
            { 
               _health.SetHealth(evt.newValue);
               currentHealth.value = _health.CurrentHealth;
                
                healthBar.value = _health.CurrentHealth / _health.MaxHealth;
            });
            
            maxHealth.BindProperty(serializedObject.FindProperty("maxHealth"));
            maxHealth.RegisterValueChangedCallback(evt =>
            {
                _health.SetMaxHealth(evt.newValue);
                maxHealth.value = _health.MaxHealth;
                currentHealth.value = _health.CurrentHealth;
                
                healthBar.value = _health.CurrentHealth / _health.MaxHealth;
            });
            

            return root;
        }
        
    }
    
    
    
}
