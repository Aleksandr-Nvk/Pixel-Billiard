using System;
using AnimationsData;
using UnityEngine;

namespace Views
{
    [Serializable]
    public class AnimatedImage
    {
        public CanvasRenderer CanvasRenderer;

        public void SetActivity(bool isShown)
        {
            var animation = AnimationsManager.FadeUi;
            
            if (isShown)
                CanvasRenderer.gameObject.SetActive(true);
            else
                animation.OnAnimationEnded += () => CanvasRenderer.gameObject.SetActive(false);
            
            animation.OnAnimationEnded += () => CanvasRenderer.gameObject.SetActive(isShown);
            animation.Play(CanvasRenderer, isShown ? 1f : 0f, 0.5f);
        }
    }
}