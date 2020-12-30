using UnityEngine;
using System;

namespace AnimationsData
{
    public class Fade
    {
        public Action OnAnimationEnded;

        private SpriteRenderer _spriteRenderer;

        private float _targetAlpha;
        
        private float _duration;
        private float _startTime;

        private bool _isLocal;
        private bool _isActive;
        
        /// <summary>
        /// Executes the animation for ONE frame. Use in Update or Update-like functions only.
        /// </summary>
        public void Execute()
        {
            if (!_isActive)
                return;

            if (Math.Abs(_spriteRenderer.color.a - _targetAlpha) > 0.0001f)
            {
                var normalizedTime = (Time.realtimeSinceStartup - _startTime) / _duration;
                var newColor = SmoothColorAlpha(_spriteRenderer.color, _targetAlpha, normalizedTime);

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
            _targetAlpha = targetAlpha;
            _duration = duration;
            
            _startTime = Time.realtimeSinceStartup;
            
            _isActive = true;
        }

        public void Pause() => _isActive = false;
        
        public void Resume() => _isActive = true;

        public void Stop()
        {
            _isActive = false;
            OnAnimationEnded?.Invoke();
            OnAnimationEnded = null;
        }
        
        private static Color SmoothColorAlpha(Color start, float end, float duration)
        {
            return new Color(start.r, start.g, start.b, Mathf.SmoothStep(start.a, end, duration));
        }
    }
}