using FieldData;
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
        
        public void Init(GameSession gameSession, MoveManager moveManager)
        {
            Show();
            
            _gameSession = gameSession;
            
            gameSession.OnSessionStarted += _ => Show();
            gameSession.OnSessionEnded += Hide;
            
            _firstPlayerView.Init(moveManager._firstPlayer, moveManager);
            _secondPlayerView.Init(moveManager._secondPlayer, moveManager);
        }
        
        public void Show()
        {
            _pauseButton.SetActivity(true);
        }

        public void Hide()
        { }
    }
}