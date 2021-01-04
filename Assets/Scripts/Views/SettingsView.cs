using UnityEngine;
using System;
using System.Collections;
using Models;
using UnityEngine.UI;

namespace Views
{
    public class SettingsView : MonoBehaviour
    {
        [SerializeField] private Animations _animations = default;

        [SerializeField] private CanvasGroup _canvasGroup = default;
        
        [SerializeField] private Button _closeButton = default;
        
        [SerializeField] private Button _musicButton = default;
        [SerializeField] private Image _musicButtonImage = default;
        [SerializeField] private Sprite _musicOnIcon = default;
        [SerializeField] private Sprite _musicOffIcon = default;
        
        [SerializeField] private Button _soundButton = default;
        [SerializeField] private Image _soundButtonImage = default;
        [SerializeField] private Sprite _soundOnIcon = default;
        [SerializeField] private Sprite _soundOffIcon = default;
        
        [SerializeField] private Button _infoButton = default;

        private Action _onShown;
        private Action _onHidden;
        
        public void Init(Settings settings)
        {
            _closeButton.onClick.AddListener(Hide);
            
            _musicButton.onClick.AddListener(settings.SwitchMusic);
            settings.OnMusicStateChanged += () => { SwitchIcon(_musicButtonImage, _musicOnIcon, _musicOffIcon); };
            
            _soundButton.onClick.AddListener(settings.SwitchSound);
            settings.OnSoundStateChanged += () => { SwitchIcon(_soundButtonImage, _soundOnIcon, _soundOffIcon); };
            
            _infoButton.onClick.AddListener(settings.GetInfo);
        }
        
        /// <summary>
        /// Shows the view and sets the onHidden action
        /// </summary>
        /// <param name="onHidden"> Action to be invoked when hiding the settings view </param>
        public void Show(Action onHidden)
        {
            _onHidden = onHidden;

            _canvasGroup.gameObject.SetActive(true);
            _animations.Fade(_canvasGroup, 1f, 0.5f);
            _canvasGroup.interactable = true;
            _canvasGroup.blocksRaycasts = true;
        }

        public void Hide()
        {
            _canvasGroup.interactable = false;
            StartCoroutine(Hide());
            
            IEnumerator Hide()
            {
                yield return _animations.Fade(_canvasGroup, 0f, 0.5f);
                gameObject.SetActive(false);
                _canvasGroup.blocksRaycasts = false;
            }

            _onHidden?.Invoke();
            _onHidden = null;
        }
        
        private void SwitchIcon(Image buttonImage, Sprite icon1, Sprite icon2)
        {
            buttonImage.sprite = buttonImage.sprite == icon1 ? icon2 : icon1;
        }
    }
}