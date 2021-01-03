using UnityEngine;
using System;

namespace AnimationsData
{
    public class Move
    {
        public Action OnAnimationEnded;

        private Transform _transform;
    
        private Vector3 _startPosition;
        private Vector3 _targetPosition;
        private Vector3 _tempStartPosition;
    
        private float _duration;
        private float _lerpTime;

        private bool _isLocal;
        private bool _condition;
        private bool _isActive;

        /// <summary>
        /// Executes the animation for ONE frame. Use in Update or Update-like functions only.
        /// </summary>
        public void Execute()
        {
            if (!_isActive)
                return;
                
            var newPosition = Vector3.Lerp(_tempStartPosition, _targetPosition, _lerpTime / _duration);
            _lerpTime += Time.unscaledDeltaTime;

            if (_condition)
            {
                if (_isLocal)
                {
                    _transform.localPosition = newPosition;
                    _condition = _transform.localPosition != _targetPosition;
                }
                else
                {
                    _transform.position = newPosition;
                    _condition = _transform.position != _targetPosition;
                }
            }
            else
            {
                Stop();
            }
        }

        /// <summary>
        /// Starts a Move animation
        /// </summary>
        /// <param name="transform"> Transform to move </param>
        /// <param name="targetPosition"> Target position </param>
        /// <param name="duration"> Animation duration in seconds </param>
        /// <param name="isLocal"> Is movement local? False by default </param>
        public void Play(Transform transform, Vector3 targetPosition, float duration, bool isLocal = false)
        {
            _transform = transform;
            _targetPosition = targetPosition;
            _duration = duration;
            _isLocal = isLocal;
            
            _condition = (_isLocal? _transform.localPosition : _transform.position) != _targetPosition;
                
            _tempStartPosition = _isLocal ? _transform.localPosition : _transform.position;

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