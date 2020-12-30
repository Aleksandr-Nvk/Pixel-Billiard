using System;
using TMPro;
using UnityEngine.UI;

namespace Views
{
    [Serializable]
    public class AnimatedInputField
    {
        public TMP_InputField InputField;

        public TextMeshProUGUI InputText;
        
        public Image InputFieldImage;

        private TMP_InputField.OnChangeEvent _onValueChanged => InputField.onValueChanged;
        private TMP_InputField.SubmitEvent _onEndEdit => InputField.onEndEdit;

        public void SetActivity(bool isShown)
        {
            InputField.interactable = isShown;
            InputFieldImage.raycastTarget = isShown;
            Animations.Fade(InputFieldImage, isShown ? 1f : 0f, 1f);
            Animations.Fade(InputText, isShown ? 1f : 0f, 1f);
        }
    }
}