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
        public Action OnSessionRestarted;
        public Action OnSessionExited;
        public Action<Player> OnSessionEnded;

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
    
        public void Start(string firstPlayerName, string secondPlayerName, bool callSilently = false)
        {
            _triangle = _ballsFactory();
            _field = _fieldFactory(_triangle);
            _cue = _cueFactory(_triangle, _field);
            
            FirstPlayer = new Player(firstPlayerName);
            SecondPlayer = new Player(secondPlayerName);
            MoveManager = new MoveManager(_field, FirstPlayer, SecondPlayer, this);

            Resume(true);

            if (!callSilently)
                OnSessionStarted?.Invoke();
        }

        public void Pause(bool callSilently = false)
        {
            Time.timeScale = 0f;
            _inputManager.StopChecking();
            
            if (!callSilently)
                OnSessionPaused?.Invoke();
        }
        
        public void Resume(bool callSilently = false)
        {
            Time.timeScale = 1f;
            _inputManager.StartChecking();
            
            if (!callSilently)
                OnSessionResumed?.Invoke();
        }

        public void Restart(bool callSilently = false)
        {
            Object.Destroy(_triangle.gameObject);
            Object.Destroy(_field.gameObject);
            Object.Destroy(_cue.gameObject);
            
            Start(FirstPlayer.Name, SecondPlayer.Name);
            
            if (!callSilently)
                OnSessionRestarted?.Invoke();
        }

        public void Exit(bool callSilently = false)
        {
            Time.timeScale = 1f;
            _inputManager.StopChecking();
            
            Object.Destroy(_triangle.gameObject);
            Object.Destroy(_field.gameObject);
            Object.Destroy(_cue.gameObject);
            
            if (!callSilently)
                OnSessionExited?.Invoke();
        }

        public void End(Player winner, bool callSilently = false)
        {
            Pause(callSilently: true);

            if (!callSilently)
                OnSessionEnded?.Invoke(winner);
        }
    }
}