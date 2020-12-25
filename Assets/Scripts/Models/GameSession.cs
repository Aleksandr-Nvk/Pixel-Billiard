using System;
using Balls;
using CueData;
using FieldData;
using Views;
using Object = UnityEngine.Object;

namespace Models
{
    public class GameSession
    {
        public Action OnSessionStarted;
        public Action OnSessionEnded;
    
        private readonly BallsFactory _ballsFactory;
        private readonly FieldFactory _fieldFactory;
        private readonly CueFactory _cueFactory;
        
        private readonly GameSessionViewFactory _gameSessionViewFactory;
        private readonly PlayerViewFactory _playerViewFactory;
        private readonly PlayerView _firstPlayerPrefab;
        private readonly PlayerView _secondPlayerViewPrefab;

        private Triangle _triangle;
        private Field _field;
        private Cue _cue;
        
        private PlayerView _firstPlayerView;
        private GameSessionView _gameSessionView;
        private PlayerView _secondPlayerView;

        public GameSession(BallsFactory ballsFactory, FieldFactory fieldFactory, CueFactory cueFactory,
            GameSessionViewFactory gameSessionViewFactory, PlayerViewFactory playerViewFactory, PlayerView firstPlayerPrefab,
            PlayerView secondPlayerViewPrefab)
        {
            _ballsFactory = ballsFactory;
            _cueFactory = cueFactory;
            _gameSessionViewFactory = gameSessionViewFactory;
            
            _playerViewFactory = playerViewFactory;
            _firstPlayerPrefab = firstPlayerPrefab;
            _secondPlayerViewPrefab = secondPlayerViewPrefab;
            _fieldFactory = fieldFactory;
        }
    
        public void StartSession()
        {
            _triangle = _ballsFactory.Create();
            _field = _fieldFactory.Create(_triangle);
            _cue = _cueFactory.Create(_triangle, _field);
            
            var firstPlayer = new Player("Alicia");
            var secondPlayer = new Player("Nic");
            var moveManager = new MoveManager(_field, firstPlayer, secondPlayer);

            _gameSessionView = _gameSessionViewFactory.Create(this, moveManager);
            _firstPlayerView = _playerViewFactory.Create(_firstPlayerPrefab, _gameSessionView, firstPlayer, moveManager);
            _secondPlayerView = _playerViewFactory.Create(_secondPlayerViewPrefab, _gameSessionView, secondPlayer, moveManager);

            InputManager.StartTracking();
        
            OnSessionStarted?.Invoke();
        }

        public void EndSession()
        {
            Object.Destroy(_triangle);
            Object.Destroy(_field);
            Object.Destroy(_cue);

            _gameSessionView.ResetView();
            _firstPlayerView.ResetView();
            _secondPlayerView.ResetView();
            
            OnSessionEnded?.Invoke();
        }
    }
}