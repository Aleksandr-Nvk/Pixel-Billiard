using System;
using UnityEngine;

namespace AnimationsData
{
    public class FadeUi
    {
        public Action OnAnimationEnded;

        private CanvasRenderer _canvasRenderer;

        private float _startAlpha;
        private float _targetAlpha;
        
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

            if (Math.Abs(_canvasRenderer.GetAlpha() - _targetAlpha) > 0.0001f)
            {
                var newAlpha = Mathf.Lerp(_startAlpha, _targetAlpha, _lerpTime / _duration);
                _lerpTime += Time.unscaledDeltaTime;
                
                _canvasRenderer.SetAlpha(newAlpha);
            }
            else
            {
                Stop();
            }
        }
        
        public void Play(CanvasRenderer canvasRendererToFade, float targetAlpha, float duration)
        {
            _canvasRenderer = canvasRendererToFade;
            _startAlpha = canvasRendererToFade.GetAlpha();
            _targetAlpha = targetAlpha;
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