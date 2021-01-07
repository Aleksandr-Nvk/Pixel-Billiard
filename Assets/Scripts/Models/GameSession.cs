using System;
using Balls;
using CueData;
using FieldData;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Models
{
    public class GameSession
    {
        public Action OnSessionStarted;
        public Action OnSessionPaused;
        public Action OnSessionResumed;
        public Action OnSessionExited;
        public Action OnSessionEnded;

        public MoveManager MoveManager { get; private set; }
        public Player FirstPlayer { get; private set; }
        public Player SecondPlayer { get; private set; }
    
        private readonly Func<Triangle> _ballsFactory;
        private readonly Func<Triangle, Field> _fieldFactory;
        private readonly Func<Triangle, Field, Cue> _cueFactory;
        
        private readonly InputManager _inputManager;

        private Triangle _triangle;
        private Field _field;
        private Cue _cue;

        public GameSession(Func<Triangle> ballsFactory, Func<Triangle, Field> fieldFactory,
            Func<Triangle, Field, Cue> cueFactory, InputManager inputManager)
        {
            _ballsFactory = ballsFactory;
            _cueFactory = cueFactory;
            _fieldFactory = fieldFactory;
            
            _inputManager = inputManager;
        }
    
        public void Start(string firstPlayerName, string secondPlayerName)
        {
            _triangle = _ballsFactory();
            _field = _fieldFactory(_triangle);
            _cue = _cueFactory(_triangle, _field);
            
            FirstPlayer = new Player(firstPlayerName);
            SecondPlayer = new Player(secondPlayerName);
            MoveManager = new MoveManager(_field, FirstPlayer, SecondPlayer);

            _inputManager.StartChecking();
        
            OnSessionStarted?.Invoke();
        }

        public void Pause()
        {
            Time.timeScale = 0f;
            _inputManager.StopChecking();
            
            OnSessionPaused?.Invoke();
        }
        
        public void Resume()
        {
            Time.timeScale = 1f;
            _inputManager.StartChecking();
            
            OnSessionResumed?.Invoke();
        }

        public void Exit()
        {
            Time.timeScale = 1f;
            _inputManager.StopChecking();
            
            Object.Destroy(_triangle.gameObject);
            Object.Destroy(_field.gameObject);
            Object.Destroy(_cue.gameObject);

            OnSessionExited?.Invoke();
        }

        public void End()
        {
            OnSessionEnded?.Invoke();
        }
    }
}