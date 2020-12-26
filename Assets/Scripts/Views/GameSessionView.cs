using System;
using FieldData;
using Models;
using UnityEngine;

namespace Views
{
    public class GameSessionView : MonoBehaviour
    {
        [SerializeField] private AnimatedButton _pauseButton = default;
        
        private GameSession _gameSession;

        private Action<MoveManager> _onSessionStarted;
        
        public void Init(GameSession gameSession)
        {
            Show();
            
            _gameSession = gameSession;
            
            _onSessionStarted = gameSession.OnSessionStarted += _ => Show();
            gameSession.OnSessionEnded += Hide;
            gameSession.OnSessionEnded += ResetView;
        }
        
        public void Show()
        {
            _pauseButton.SetActivity(true);
        }

        public void Hide()
        {
            _pauseButton.SetActivity(false);
        }

        public void ResetView()
        {
            _gameSession.OnSessionStarted -= _onSessionStarted;
            _gameSession.OnSessionEnded -= Hide;
            _gameSession.OnSessionEnded -= ResetView;
        }
    }
}