using UnityEngine;
using System;

namespace AnimationsData
{
    public class FadeSprite
    {
        public Action OnAnimationEnded;
        
        private SpriteRenderer _spriteRenderer;

        private Color _startColor;
        private Color _targetColor;
        
        private float _duration;
        private float _lerpTime;

        private bool _isLocal;
        private bool _isActive;
        
        /// <summary>
        /// Executes the animation for ONE frame. Use in Update or Update-like functions only.
        /// </summary>
        public void Execute()
        {
            if (!_isActive)
                return;

            if (Math.Abs(_spriteRenderer.color.a - _targetColor.a) > 0.0001f)
            {
                var newColor = Color.Lerp(_startColor, _targetColor, _lerpTime / _duration);
                _lerpTime += Time.unscaledDeltaTime;
                
                _spriteRenderer.color = newColor;
            }
            else
            {
                Stop();
            }
        }
        
        public void Play(SpriteRenderer spriteRendererToFade, float targetAlpha, float duration)
        {
            _spriteRenderer = spriteRendererToFade;
            _startColor = _spriteRenderer.color;
            _targetColor = new Color(_spriteRenderer.color.r, _spriteRenderer.color.g, _spriteRenderer.color.b, targetAlpha);
            _duration = duration;
            
            _isActive = true;
        }

        public void Pause() => _isActive = false;
        
        public void Resume() => _isActive = true;

        public void Stop()
        {
            _isActive = false;
            _lerpTime = 0f;

            OnAnimationEnded?.Invoke();
            OnAnimationEnded = null;
        }
    }
}