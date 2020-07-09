using System;
using System.Collections;
using UnityEngine;

namespace Tween_Library.Scripts.Effects
{
    public class SnapToEffect : IUiEffect
    {
        public event Action<IUiEffect> OnComplete;
        
        private readonly RectTransform _transform;
        private Vector2 _snapToSize;

        public SnapToEffect(RectTransform transform, Vector2 snapToSize)
        {
            _transform = transform;
            _snapToSize = snapToSize;
        }


        public void UpdateSnapSize(Vector2 snapToSize)
        {
            _snapToSize = snapToSize;
        }
        
        
        public IEnumerator Execute()
        {
            _transform.sizeDelta = _snapToSize;
            yield break;
        }

    }
}