using FieldData;
using Models;
using UnityEngine;
using TMPro;

namespace Views
{
    public class HomeView : MonoBehaviour
    {
        [Header("View data")]

        [SerializeField] private TextMeshProUGUI _title = default;
        
        [SerializeField] private AnimatedButton _settingsButton = default;
        [SerializeField] private AnimatedButton _playButton = default;

        [Header("Other views")]

        [SerializeField] private SettingsView _settingsView = default;

        [SerializeField] private GameSessionView _gameSessionView = default;

        [SerializeField] private PlayerView _firstPlayerView = default;
        [SerializeField] private PlayerView _secondPlayerView = default;

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
                gameSession.OnSessionStarted += moveManager =>
                {
                    _gameSessionView.Init(gameSession);
                    InitPlayers(moveManager);
                };
                gameSession.StartSession();
            });
        }
        
        public void Show()
        {
            Animations.Fade(_title, 1f, 0.5f);

            _settingsButton.SetActivity(true);
            _playButton.SetActivity(true);
        }

        public void Hide()
        {
            Animations.Fade(_title, 0f, 0.5f);
            
            _settingsButton.SetActivity(false);
            _playButton.SetActivity(false);
        }

        private void InitPlayers(MoveManager moveManager)
        {
            var firstPlayer = new Player("Alicia");
            var secondPlayer = new Player("Nic");
            moveManager.FirstPlayer = firstPlayer;
            moveManager.SecondPlayer = secondPlayer;
                    
            _firstPlayerView.Init(firstPlayer, moveManager);
            _firstPlayerView.Show();
            _secondPlayerView.Init(secondPlayer, moveManager);
            _secondPlayerView.Show();
        }
    }
}