using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimationsData
{
    public class AnimationsManager : MonoBehaviour
    {
        [SerializeField] private int _moveAnimationsPoolCapacity = default;
        [SerializeField] private int _fadeAnimationsPoolCapacity = default;
        
        private readonly Queue<Move> _moveAnimationsPool = new Queue<Move>();
        private readonly List<Move> _activeMoveAnimations = new List<Move>();
        
        private readonly Queue<Fade> _fadeAnimationsPool = new Queue<Fade>();
        private readonly List<Fade> _activeFadeAnimations = new List<Fade>();


        public Move Move
        {
            get
            {
                var move = _moveAnimationsPool.Dequeue();
                _activeMoveAnimations.Add(move);
                
                return move;
            }
        }

        public Fade Fade
        {
            get
            {
                var fade = _fadeAnimationsPool.Dequeue();
                _activeFadeAnimations.Add(fade);
                
                return fade;
            }
        }

        private void Start()
        {
            for (var i = 0; i < _moveAnimationsPoolCapacity; i++)
            {
                var move = new Move();
                move.OnAnimationEnded += () =>
                {
                    _activeMoveAnimations.Remove(move);
                    _moveAnimationsPool.Enqueue(move);
                };
                
                _moveAnimationsPool.Enqueue(move);
            }
            for (var i = 0; i < _fadeAnimationsPoolCapacity; i++)
            {
                var fade = new Fade();
                fade.OnAnimationEnded += () =>
                {
                    _activeFadeAnimations.Remove(fade);
                    _fadeAnimationsPool.Enqueue(fade);
                };
                
                _fadeAnimationsPool.Enqueue(fade);
            }
        }

        private void Update()
        {
            foreach (var activeMoveAnimation in _activeMoveAnimations.ToList()) activeMoveAnimation.Execute();
            foreach (var activeFadeAnimation in _activeFadeAnimations.ToList()) activeFadeAnimation.Execute();
        }
    }
}