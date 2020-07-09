using System;
using System.Collections;
using System.Collections.Generic;
using Tween_Library.Scripts;
using Tween_Library.Scripts.Effects;
using UnityEngine;

public class UiEffectDispatcher : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector2 snapSize;

    private SnapToEffect snapToEffect;
    private EffectBuilder effectBuilder;

    private void Start()
    {
        snapToEffect = new SnapToEffect(rectTransform, snapSize);
        effectBuilder = new EffectBuilder(this)
            .AddEffect(snapToEffect);
    }


    private void OnValidate()
    {
        snapToEffect?.UpdateSnapSize(snapSize);
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            effectBuilder.ExecuteEffects();
        }
    }
}