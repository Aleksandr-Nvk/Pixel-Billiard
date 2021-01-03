using System;
using AnimationsData;
using TMPro;
using UnityEngine;

namespace Views
{
    [Serializable]
    public class AnimatedText
    {
        public CanvasRenderer CanvasRenderer;

        public TextMeshProUGUI TextMeshPro;
        
        public void SetActivity(bool isShown)
        {
            var animation = AnimationsManager.FadeUi;

            if (isShown)
                CanvasRenderer.gameObject.SetActive(true);
            else
                animation.OnAnimationEnded += () => CanvasRenderer.gameObject.SetActive(false);
            
            animation.Play(CanvasRenderer, isShown ? 1f : 0f, 0.5f);
        }
    }
}