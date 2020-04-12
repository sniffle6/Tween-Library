using System;
using System.Collections;

namespace Tween_Library.Scripts
{
    public interface IUiEffect
    {
        IEnumerator Execute();
        event Action<IUiEffect> OnComplete;
    }
}