using System;
using Balls;
using CueData;
using FieldData;
using Object = UnityEngine.Object;

namespace Models
{
    public class GameSession
    {
        public Action<MoveManager> OnSessionStarted;
        public Action OnSessionEnded;
    
        private readonly BallsFactory _ballsFactory;
        private readonly FieldFactory _fieldFactory;
        private readonly CueFactory _cueFactory;

        private Triangle _triangle;
        private Field _field;
        private Cue _cue;

        public GameSession(BallsFactory ballsFactory, FieldFactory fieldFactory, CueFactory cueFactory)
        {
            _ballsFactory = ballsFactory;
            _cueFactory = cueFactory;
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

            InputManager.StartTracking();
        
            OnSessionStarted?.Invoke(moveManager);
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