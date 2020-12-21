using UnityEngine;

namespace Balls
{
    public class Triangle : MonoBehaviour
    {
        [SerializeField] private Ball[] _allBalls = default;
        [SerializeField] private WhiteBall _whiteBall = default;

        public WhiteBall WhiteBall => _whiteBall;
        public Ball[] AllBalls => _allBalls;

        public void Init(AudioManager audioManager)
        {
            foreach (var ball in _allBalls)
            {
                ball.Init(audioManager);
            }
        }
    }
}