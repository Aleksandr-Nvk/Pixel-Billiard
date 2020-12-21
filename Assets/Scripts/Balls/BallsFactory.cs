using UnityEngine;

namespace Balls
{
    public class BallsFactory
    {
        private readonly Triangle _trianglePrefab;

        private readonly AudioManager _audioManager;
            
        public BallsFactory(Triangle trianglePrefab, AudioManager audioManager)
        {
            _trianglePrefab = trianglePrefab;
            _audioManager = audioManager;
        }

        public Triangle Create()
        {
            var triangle = Object.Instantiate(_trianglePrefab);
            triangle.Init(_audioManager);

            return triangle;
        }
    }
}