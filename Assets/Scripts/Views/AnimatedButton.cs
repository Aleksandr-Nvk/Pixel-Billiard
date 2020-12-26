using System.ComponentModel;
using UnityEngine.UI;
using UnityEngine;
using System;

namespace Views
{
    [Serializable]
    public class AnimatedButton
    {
        public Button Button = default;
        
        public Image ButtonImage = default;

        public Button.ButtonClickedEvent onClick => Button.onClick;

        public void SetActivity(bool isShown)
        {
            Button.interactable = isShown;
            Animations.Fade(ButtonImage, isShown ? 1f : 0f, 1f);
        }

        public void SwitchIcon(Sprite sprite1, Sprite sprite2)
        {
            ButtonImage.sprite = ButtonImage.sprite == sprite1 ? sprite2 : sprite1;
        }
    }
}