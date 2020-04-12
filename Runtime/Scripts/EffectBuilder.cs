using System;
using System.Collections.Generic;
using UnityEngine;

namespace Tween_Library.Scripts
{
    public class EffectBuilder
    {
        private MonoBehaviour Owner { get; }
        private readonly List<IUiEffect> _effects = new List<IUiEffect>();

        private int _completedEffects = 0;

        public event Action OnAllEffectsComplete;
        
        public EffectBuilder(MonoBehaviour owner)
        {
            Owner = owner;
        }
        
        public EffectBuilder AddEffect(IUiEffect effect)
        {
            _effects.Add(effect);
            effect.OnComplete += OnEffectComplete;
            return this;
        }

        public void ExecuteEffects()
        {
            Owner.StopAllCoroutines();
            foreach (var effect in _effects)
            {
                Owner.StartCoroutine(effect.Execute());
            }
        }

        private void OnEffectComplete(IUiEffect effect)
        {
            _completedEffects += 1;
            if (_completedEffects < _effects.Count)
                return;
            AllEffectsComplete();
        }

        private void AllEffectsComplete()
        {
            _completedEffects = 0;
            OnAllEffectsComplete?.Invoke();
        }
    }
}