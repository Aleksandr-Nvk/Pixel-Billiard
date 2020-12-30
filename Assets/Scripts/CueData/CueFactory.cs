using AnimationsData;
using Balls;
using FieldData;
using UnityEngine;

namespace CueData
{
    public class CueFactory
    {
        private readonly Cue _cuePrefab;
        private readonly AnimationsManager _animationsManager;

        public CueFactory(Cue cuePrefab, AnimationsManager animationsManager)
        {
            _cuePrefab = cuePrefab;
            _animationsManager = animationsManager;
        }

        public Cue Create(Triangle triangle, Field field)
        {
            var cue = Object.Instantiate(_cuePrefab);
            cue.Init(triangle.WhiteBall, field, _animationsManager);
            
            return cue;
        }
    }
}