using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Tween_Library.Scripts.Effects
{
    public class FlashColorEffect : IUiEffect
    {
        public event Action<IUiEffect> OnComplete;
        private YieldInstruction Wait { get;  }
        private Color DefaultColor { get; }
        private Color TakeDamageColor { get;  }
        private Image SliderFill { get; }

        
        public FlashColorEffect(Color defaultColor, Color takeDamageColor, Image sliderFill, YieldInstruction wait, Action<IUiEffect> onComplete = null)
        {
            DefaultColor = defaultColor;
            TakeDamageColor = takeDamageColor;
            SliderFill = sliderFill;
            Wait = wait;
            OnComplete += onComplete;
        }
        
        public IEnumerator Execute()
        {
            var time = 0f;
            while (SliderFill.color != TakeDamageColor)
            {
                time += Time.deltaTime*20;
                var color = Color.Lerp(DefaultColor, TakeDamageColor, time);
                SliderFill.color = color;
                yield return null;
            }

            yield return Wait;
            
            time = 0f;
            while (SliderFill.color != DefaultColor)
            {
                time += Time.deltaTime*10;
                var color = Color.Lerp(TakeDamageColor, DefaultColor, time);
                SliderFill.color = color;
                yield return null;
            }
            
            OnComplete?.Invoke(this);
        }
    }
}