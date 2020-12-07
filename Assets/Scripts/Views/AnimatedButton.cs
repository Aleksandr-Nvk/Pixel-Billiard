using System;
using UnityEngine.UI;

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
    }
}