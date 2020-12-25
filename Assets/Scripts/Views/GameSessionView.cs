using Models;
using UnityEngine;

namespace Views
{
    public class GameSessionView : MonoBehaviour
    {
        [SerializeField] private AnimatedButton _pauseButton = default;
        
        private GameSession _gameSession;
        
        public void Init(GameSession gameSession)
        {
            Show();
            
            _gameSession = gameSession;
            
            gameSession.OnSessionStarted += Show;
            gameSession.OnSessionEnded += Hide;
            gameSession.OnSessionEnded += ResetView;
        }
        
        public void Show()
        {
            _pauseButton.SetActivity(true);
        }

        public void Hide()
        { }

        public void ResetView()
        {
            _gameSession.OnSessionStarted -= Show;
            _gameSession.OnSessionEnded -= Hide;
            _gameSession.OnSessionEnded -= ResetView;
        }
    }
}