using Models;
using UnityEngine;

namespace Views
{
    public class GameSessionView : MonoBehaviour
    {
        [SerializeField] private AnimatedButton _pauseButton = default;
        
        [SerializeField] private PlayerView _firstPlayerView = default;
        [SerializeField] private PlayerView _secondPlayerView = default;
        
        private GameSession _gameSession;
        
        public void Init(GameSession gameSession)
        {
            _gameSession = gameSession;
            
            gameSession.OnSessionStarted += Show;
            gameSession.OnSessionStarted += InitPlayers;
            
            gameSession.OnSessionEnded += Hide;
            gameSession.OnSessionEnded += ResetView;
        }
        
        public void Show()
        {
            gameObject.SetActive(true);
            
            _pauseButton.SetActivity(true);
            _firstPlayerView.Show();
            _secondPlayerView.Show();
        }

        public void Hide()
        {
            _pauseButton.SetActivity(false);
            _firstPlayerView.Hide();
            _secondPlayerView.Hide();
        }

        public void ResetView()
        {
            _gameSession.OnSessionEnded -= Hide;
            _gameSession.OnSessionEnded -= ResetView;
        }
        
        private void InitPlayers()
        {
            _firstPlayerView.Init(_gameSession.FirstPlayer, _gameSession.MoveManager);
            _firstPlayerView.Show();
            _secondPlayerView.Init(_gameSession.SecondPlayer, _gameSession.MoveManager);
            _secondPlayerView.Show();
        }
    }
}