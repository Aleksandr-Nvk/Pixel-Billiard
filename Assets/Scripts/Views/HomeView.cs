using Models;
using UnityEngine;
using TMPro;

namespace Views
{
    public class HomeView : MonoBehaviour
    {
        [Header("View data")]

        [SerializeField] private TextMeshProUGUI _title1 = default;
        [SerializeField] private TextMeshProUGUI _title2 = default;
        
        [SerializeField] private AnimatedButton _settingsButton = default;
        [SerializeField] private AnimatedButton _playButton = default;

        [Header("Other views")]

        [SerializeField] private SettingsView _settingsView = default;
        
        public void Init(GameSession gameSession)
        {
            _settingsButton.onClick.AddListener(() =>
            {
                Hide();
                _settingsView.Show(Show);
            });
            
            _playButton.onClick.AddListener(() =>
            {
                Hide();
                gameSession.StartSession();
            });
        }
        
        public void Show()
        {
            Animations.Fade(_title1, 1f, 0.5f);
            Animations.Fade(_title2, 1f, 0.5f);

            _settingsButton.SetActivity(true);
            _playButton.SetActivity(true);
        }

        public void Hide()
        {
            Animations.Fade(_title1, 0f, 0.5f);
            Animations.Fade(_title2, 0f, 0.5f);
            
            _settingsButton.SetActivity(false);
            _playButton.SetActivity(false);
        }
    }
}