using Balls;
using FieldData;
using UnityEngine;

namespace CueData
{
    public class CueFactory
    {
        private readonly Cue _cuePrefab;

        public CueFactory(Cue cuePrefab)
        {
            _cuePrefab = cuePrefab;
        }

        public Cue Create(Triangle triangle, Field field)
        {
            var cue = Object.Instantiate(_cuePrefab);
            cue.Init(triangle.WhiteBall, field);
            
            return cue;
        }
    }
}