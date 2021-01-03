using Models;
using UnityEngine;

namespace Views
{
    public class HomeView : MonoBehaviour
    {
        [Header("View data")]

        [SerializeField] private AnimatedText _title = default;
        
        [SerializeField] private AnimatedButton _settingsButton = default;
        [SerializeField] private AnimatedButton _playButton = default;

        [SerializeField] private AnimatedInputField _firstPlayerNameField = default;
        [SerializeField] private AnimatedInputField _secondPlayerNameField = default;
        
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
                
                var firstPlayerName = StringValidator.ValidateInput(_firstPlayerNameField.InputField.text, "Player1");
                var secondPlayerName = StringValidator.ValidateInput(_secondPlayerNameField.InputField.text, "Player2");
                gameSession.StartSession(firstPlayerName, secondPlayerName);
            });
        }
        
        public void Show()
        {
            _title.SetActivity(true);

            _settingsButton.SetActivity(true);
            _playButton.SetActivity(true);
            
            _firstPlayerNameField.SetActivity(true);
            _secondPlayerNameField.SetActivity(true);
        }

        public void Hide()
        {
            _title.SetActivity(false);
            
            _settingsButton.SetActivity(false);
            _playButton.SetActivity(false);
            
            _firstPlayerNameField.SetActivity(false);
            _secondPlayerNameField.SetActivity(false);
        }
    }
}