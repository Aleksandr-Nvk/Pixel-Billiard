using UnityEngine.UI;
using UnityEngine;
using System;
using Models;

namespace Views
{
    public class SettingsView : MonoBehaviour
    {
        [Header("View data")]
        
        [SerializeField] private Image _settingsView = default;
        
        [SerializeField] private AnimatedButton _closeButton = default;

        [SerializeField] private AnimatedButton _musicButton = default;
        [SerializeField] private Sprite _musicOnIcon = default;
        [SerializeField] private Sprite _musicOffIcon = default;
        
        [SerializeField] private AnimatedButton _soundButton = default;
        [SerializeField] private Sprite _soundOnIcon = default;
        [SerializeField] private Sprite _soundOffIcon = default;
        
        [SerializeField] private AnimatedButton _infoButton = default;

        private Action _onShown;
        private Action _onHidden;
        
        public void Init(Settings settings)
        {
            _closeButton.onClick.AddListener(Hide);
            
            _musicButton.onClick.AddListener(settings.SwitchMusic);
            _musicButton.onClick.AddListener(SwitchMusicButtonIcon);
            
            _soundButton.onClick.AddListener(settings.SwitchSound);
            _soundButton.onClick.AddListener(SwitchSoundButtonIcon);

            _infoButton.onClick.AddListener(settings.GetInfo);
        }
        
        /// <summary>
        /// Shows the view and sets the onHidden action
        /// </summary>
        /// <param name="onHidden"> Action to be invoked when hiding the settings view </param>
        public void Show(Action onHidden)
        {
            _onHidden = onHidden;
            
            Animations.Fade(_settingsView, 1f, 0.5f);
            
            _closeButton.SetActivity(true);
            _soundButton.SetActivity(true);
            _musicButton.SetActivity(true);
            _infoButton.SetActivity(true);
        }

        public void Hide()
        {
            Animations.Fade(_settingsView, 0f, 0.5f);
            
            _closeButton.SetActivity(false);
            _soundButton.SetActivity(false);
            _musicButton.SetActivity(false);
            _infoButton.SetActivity(false);
            
            _onHidden?.Invoke();
            _onHidden = null;
        }

        private void SwitchMusicButtonIcon()
        {
            _musicButton.ButtonImage.sprite = _musicButton.ButtonImage.sprite == _musicOnIcon
                ? _musicOffIcon
                : _musicOnIcon;
        }
        
        private void SwitchSoundButtonIcon()
        {
            _soundButton.ButtonImage.sprite = _soundButton.ButtonImage.sprite == _soundOnIcon
                ? _soundOffIcon
                : _soundOnIcon;
        }
    }
}