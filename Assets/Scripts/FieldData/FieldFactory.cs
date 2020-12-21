using Balls;
using UnityEngine;

namespace FieldData
{
    public class FieldFactory
    {
        private readonly Field _fieldPrefab;
        
        public FieldFactory(Field fieldPrefab)
        {
            _fieldPrefab = fieldPrefab;
        }

        public Field Create(Triangle triangle)
        {
            var field = Object.Instantiate(_fieldPrefab);
            field.Init(triangle.AllBalls, triangle.WhiteBall);

            return field;
        }
    }
}