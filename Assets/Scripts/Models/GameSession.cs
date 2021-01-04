using System;
using Balls;
using CueData;
using FieldData;
using Object = UnityEngine.Object;

namespace Models
{
    public class GameSession
    {
        public Action OnSessionStarted;
        public Action OnSessionEnded;

        public MoveManager MoveManager { get; private set; }
        public Player FirstPlayer { get; private set; }
        public Player SecondPlayer { get; private set; }
    
        private readonly Func<Triangle> _ballsFactory;
        private readonly Func<Triangle, Field> _fieldFactory;
        private readonly Func<Triangle, Field, Cue> _cueFactory;

        private Triangle _triangle;
        private Field _field;
        private Cue _cue;

        public GameSession(Func<Triangle> ballsFactory, Func<Triangle, Field> fieldFactory,
            Func<Triangle, Field, Cue> cueFactory)
        {
            _ballsFactory = ballsFactory;
            _cueFactory = cueFactory;
            _fieldFactory = fieldFactory;
        }
    
        public void StartSession(string firstPlayerName, string secondPlayerName)
        {
            _triangle = _ballsFactory();
            _field = _fieldFactory(_triangle);
            _cue = _cueFactory(_triangle, _field);
            
            FirstPlayer = new Player(firstPlayerName);
            SecondPlayer = new Player(secondPlayerName);
            MoveManager = new MoveManager(_field, FirstPlayer, SecondPlayer);

            InputManager.StartTracking();
        
            OnSessionStarted?.Invoke();
        }

        public void EndSession()
        {
            Object.Destroy(_triangle);
            Object.Destroy(_field);
            Object.Destroy(_cue);

            OnSessionEnded?.Invoke();
        }
    }
}