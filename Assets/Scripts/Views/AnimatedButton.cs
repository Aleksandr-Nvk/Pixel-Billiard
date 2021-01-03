using UnityEngine.UI;
using UnityEngine;
using System;
using AnimationsData;

namespace Views
{
    [Serializable]
    public class AnimatedButton
    {
        public CanvasRenderer CanvasRenderer;

        public Button Button;

        public Image ButtonImage;
        
        public Button.ButtonClickedEvent onClick => Button.onClick;

        public void SetActivity(bool isShown)
        {
            Button.interactable = isShown;
            
            var animation = AnimationsManager.FadeUi;
            
            if (isShown)
                CanvasRenderer.gameObject.SetActive(true);
            else
                animation.OnAnimationEnded += () => CanvasRenderer.gameObject.SetActive(false);
            
            animation.OnAnimationEnded += () => CanvasRenderer.gameObject.SetActive(isShown);
            animation.Play(CanvasRenderer, isShown ? 1f : 0f, 0.5f);
        }

        public void SwitchIcon(Sprite sprite1, Sprite sprite2)
        {
            ButtonImage.sprite = ButtonImage.sprite == sprite1 ? sprite2 : sprite1;
        }
    }
}