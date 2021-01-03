using System;
using AnimationsData;
using TMPro;
using UnityEngine;

namespace Views
{
    [Serializable]
    public class AnimatedInputField
    {
        public CanvasRenderer CanvasRenderer;
        
        public TMP_InputField InputField;
        
        private TMP_InputField.OnChangeEvent _onValueChanged => InputField.onValueChanged;
        private TMP_InputField.SubmitEvent _onEndEdit => InputField.onEndEdit;

        public void SetActivity(bool isShown)
        {
            InputField.interactable = isShown;
            
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