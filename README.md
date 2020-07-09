# Tween-Library
A simple library for tweening UI
===============================

# How to use

1) Create a c# script for the Ui object you want to affect
2) On that script add a new EffectBuilder
3) Add effects to the effectBuilder with
`effectBuilder.AddEffect(*effect type*);`
4) Effects can be chain built, but all fire at once.
```
effectBuilder.AddEffect(*firstEffect*).AddEffect(*second effect*).OnAllEffectsComplete += AllEffectsComplete;
```

# Example
```
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


        private void OnEffectComplete(IUiEffect effect)
        {
            print($"Completed {effect}!");
        }

        private void OnAllEffectsComplete()
        {
            print("Completed all effects!");
        }
```
