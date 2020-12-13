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

        public Sprite ButtonImageVariant1 = default;
        public Sprite ButtonImageVariant2 = default;

        public Button.ButtonClickedEvent onClick => Button.onClick;

        public void SetActivity(bool isShown)
        {
            Button.interactable = isShown;
            Animations.Fade(ButtonImage, isShown ? 1f : 0f, 1f);
        }

        public void SwitchIcon()
        {
            if (ButtonImageVariant1 != null && ButtonImageVariant2 != null)
            {
                ButtonImage.sprite = ButtonImage.sprite == ButtonImageVariant1
                    ? ButtonImageVariant2
                    : ButtonImageVariant1;
            }
            else
            {
                throw new WarningException($"{Button.gameObject.name} has no button image variants!");
            }
        }
    }
}