using UnityEngine;
using UnityEngine.UI;

namespace Views
{
    public class SettingsView : MonoBehaviour
    {
        [Header("View data")]
        
        [SerializeField] private Image _settingsView = default;
        
        [SerializeField] private AnimatedButton _closeButton = default;
        [SerializeField] private AnimatedButton _soundButton = default;
        [SerializeField] private AnimatedButton _musicButton = default;
        [SerializeField] private AnimatedButton _infoButton = default;

        [Header("Other views")]
        
        [SerializeField] private HomeView _homeView = default;

        public void Init()
        {
            _closeButton.onClick.AddListener(Hide);
            _closeButton.onClick.AddListener(_homeView.Show);
        }
        
        public void Show()
        {
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
        }
    }
}