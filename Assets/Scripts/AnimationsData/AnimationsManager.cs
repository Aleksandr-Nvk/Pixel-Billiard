using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace AnimationsData
{
    public class AnimationsManager : MonoBehaviour
    {
        [SerializeField] private int _moveAnimationsPoolCapacity = default;
        [SerializeField] private int _fadeSpriteAnimationsPoolCapacity = default;
        [SerializeField] private int _fadeUiAnimationsPoolCapacity = default;
        
        private static readonly Queue<Move> _moveAnimationsPool = new Queue<Move>();
        private static readonly List<Move> _activeMoveAnimations = new List<Move>();
        
        private static readonly Queue<FadeSprite> _fadeSpriteAnimationsPool = new Queue<FadeSprite>();
        private static readonly List<FadeSprite> _activeFadeSpriteAnimations = new List<FadeSprite>();
        
        private static readonly Queue<FadeUi> _fadeUiAnimationsPool = new Queue<FadeUi>();
        private static readonly List<FadeUi> _activeFadeUiAnimations = new List<FadeUi>();

        public static Move Move
        {
            get
            {
                var move = _moveAnimationsPool.Dequeue();
                
                move.OnAnimationEnded += () =>
                {
                    _activeMoveAnimations.Remove(move);
                    _moveAnimationsPool.Enqueue(move);
                };
                
                _activeMoveAnimations.Add(move);
                
                return move;
            }
        }

        public static FadeSprite FadeSprite
        {
            get
            {
                var fadeSprite = _fadeSpriteAnimationsPool.Dequeue();
                
                fadeSprite.OnAnimationEnded += () =>
                {
                    _activeFadeSpriteAnimations.Remove(fadeSprite);
                    _fadeSpriteAnimationsPool.Enqueue(fadeSprite);
                };
                
                _activeFadeSpriteAnimations.Add(fadeSprite);
                
                return fadeSprite;
            }
        }
        
        public static FadeUi FadeUi
        {
            get
            {
                var fadeUi = _fadeUiAnimationsPool.Dequeue();
                
                fadeUi.OnAnimationEnded += () =>
                {
                    _activeFadeUiAnimations.Remove(fadeUi);
                    _fadeUiAnimationsPool.Enqueue(fadeUi);
                };
                
                _activeFadeUiAnimations.Add(fadeUi);
                
                return fadeUi;
            }
        }

        private void Start()
        {
            for (var i = 0; i < _moveAnimationsPoolCapacity; i++)
            {
                var move = new Move();
                _moveAnimationsPool.Enqueue(move);
            }
            
            for (var i = 0; i < _fadeSpriteAnimationsPoolCapacity; i++)
            {
                var fadeSprite = new FadeSprite();
                _fadeSpriteAnimationsPool.Enqueue(fadeSprite);
            }
            
            for (var i = 0; i < _fadeUiAnimationsPoolCapacity; i++)
            {
                var fadeUi = new FadeUi();
                _fadeUiAnimationsPool.Enqueue(fadeUi);
            }
        }

        private void Update()
        {
            foreach (var activeMoveAnimation in _activeMoveAnimations.ToList())
                activeMoveAnimation.Execute();
            
            foreach (var activeFadeSpriteAnimation in _activeFadeSpriteAnimations.ToList())
                activeFadeSpriteAnimation.Execute();
            
            foreach (var activeFadeUiAnimation in _activeFadeUiAnimations.ToList())
                activeFadeUiAnimation.Execute();
        }
    }
}