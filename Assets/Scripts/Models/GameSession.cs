using Object = UnityEngine.Object;
using UnityEngine.Advertisements;
using UnityEngine;
using FieldData;
using CueData;
using System;
using Balls;

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
            inputManager.OnEscapeButtonDown += Pause;
        }
    
        public void Start(string firstPlayerName, string secondPlayerName)
        {
            _triangle = _ballsFactory();
            _field = _fieldFactory(_triangle);
            _cue = _cueFactory(_triangle, _field);
            
            FirstPlayer = new Player(firstPlayerName);
            SecondPlayer = new Player(secondPlayerName);
            MoveManager = new MoveManager(_field, FirstPlayer, SecondPlayer, this);

            ResumeInternal();
            
            OnSessionStarted?.Invoke();
        }

        public void Pause()
        {
            PauseInternal();
            OnSessionPaused?.Invoke();
        }
        private void PauseInternal()
        {
            Time.timeScale = 0f;
            _inputManager.StopChecking();
        }
        
        public void Resume()
        {
            ResumeInternal();
            OnSessionResumed?.Invoke();
        }
        private void ResumeInternal()
        {
            Time.timeScale = 1f;
            _inputManager.StartChecking();
        }

        public void Restart()
        {
            Object.Destroy(_triangle.gameObject);
            Object.Destroy(_field.gameObject);
            Object.Destroy(_cue.gameObject);
            
            Start(FirstPlayer.Name, SecondPlayer.Name);
            
            OnSessionRestarted?.Invoke();
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

        public void End(Player winner)
        {
            PauseInternal();
            Advertisement.Show("video");
            
            OnSessionEnded?.Invoke(winner);
        }
    }
}