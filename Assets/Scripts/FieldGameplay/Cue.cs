using System.Collections.Generic;
using UnityEngine;
using System;
using Balls;

namespace FieldGameplay
{
    public class Cue : MonoBehaviour
    {
        [Header("Settings")]

        [Range(0f, 20f)]
        [SerializeField] private float _force = 10f;

        [Range(0f, 20f)]
        [SerializeField] private float _sensitivity = 10f;

        [Range(-5f, 5f)]
        [SerializeField] private float _maxCueOffset = 1.5f;
    
        [Header("Data")]
        
        [SerializeField] private Transform _cuePeak = default;
        [SerializeField] private Transform _cue = default;
        
        [SerializeField] private SpriteRenderer _cueRenderer = default;

        private WhiteBall _whiteBall = default;
        private Field _field = default;

        private bool CanMove = true;
        private bool _isTouchUp;
    
        private Vector3 _touchDownPosition;
        private Vector3 _touchDragPosition;
        private Vector3 _touchUpPosition;
    
        private Vector3 _currentCuePosition;
        private Vector3 _currentCuePeakRotation;
        
        private float _currentForce;

        private Coroutine _cueFadeAnimation;

        #region Lambdas

        private Action<List<Ball>> _onBallsStopped;

        #endregion
    
        public void Init(WhiteBall whiteBall, Field field)
        {
            _whiteBall = whiteBall;
            _field = field;
            
            _cuePeak.position = _whiteBall.gameObject.transform.position;
            _currentCuePosition = _cue.position;
            _currentCuePeakRotation = _cuePeak.eulerAngles;

            InputManager.OnTouchDown += SetTouchDownData;
            InputManager.OnTouchDrag += Move;
            InputManager.OnTouchDrag += Rotate;
            InputManager.OnTouchUp += SetTouchUpData;

            _whiteBall.OnReset += AlignWithWhiteBall;

            _onBallsStopped = _field.OnBallsStopped += _ =>
            {
                AlignWithWhiteBall();
                CanMove = true;
            };
        }

        private void FixedUpdate()
        {
            if (_isTouchUp && CanMove)
            {
                Pull();
            
                _isTouchUp = false;
                CanMove = false;
            }
        }

        private void OnDestroy()
        {
            InputManager.OnTouchDown -= SetTouchDownData;
            InputManager.OnTouchDrag -= Move;
            InputManager.OnTouchDrag -= Rotate;
            InputManager.OnTouchUp -= SetTouchUpData;

            _whiteBall.OnReset -= AlignWithWhiteBall;

            _field.OnBallsStopped -= _onBallsStopped;
        }

        /// <summary>
        /// Rotates the cue depending on touch position
        /// </summary>
        private void Rotate(Vector3 touchDragPosition)
        {
            if (CanMove)
            {
                var tempTransform = _cuePeak.transform;
                tempTransform.LookAt(touchDragPosition, Vector3.back);
        
                _currentCuePeakRotation.z = tempTransform.eulerAngles.z;
                _cuePeak.transform.eulerAngles = -_currentCuePeakRotation;
            }
        }

        /// <summary>
        /// Moves the cue depending on touch position
        /// </summary>
        private void Move(Vector3 touchDragPosition)
        {
            if (CanMove)
            {
                var touchDownRadius = Vector2.Distance(_whiteBall.gameObject.transform.position, _touchDownPosition);
                var touchDragRadius = Vector2.Distance(_whiteBall.gameObject.transform.position, touchDragPosition);
        
                var offset = (touchDownRadius - touchDragRadius) * _sensitivity;
                var clampedOffset = Mathf.Clamp(offset, -_maxCueOffset, 0);
                _currentCuePosition.y = clampedOffset;
                _cue.localPosition = _currentCuePosition;

                _currentForce = Mathf.Abs(clampedOffset) * _force;
            }
        }
    
        /// <summary>
        /// Sets cue position to the white ball position
        /// </summary>
        private void AlignWithWhiteBall()
        {
            _cuePeak.position = _whiteBall.gameObject.transform.position;
            _cuePeak.rotation = Quaternion.identity;
        
            Animations.Stop(_cueFadeAnimation);
            Animations.Fade(_cueRenderer, 1f, 0.5f);
        }
    
        /// <summary>
        /// Pulls the white ball with specific force to the specific direction
        /// </summary>
        private void Pull()
        {
            Vector2 direction = (-(_touchUpPosition - _cuePeak.position)).normalized;
            _whiteBall.Hit(_currentForce * direction, ForceMode2D.Impulse);
            
            Animations.Move(_cue, Vector3.zero, 0.025f, true);
            _cueFadeAnimation = Animations.Fade(_cueRenderer, 0f, 0.5f);
            
            InputManager.StopTracking();
            
            _field.CheckBallsMovement();
        }

        #region Input
    
        private void SetTouchDownData(Vector3 touchPosition)
        {
            _touchDownPosition = touchPosition;
        }
        private void SetTouchUpData(Vector3 touchPosition)
        {
            _touchUpPosition = touchPosition;
            
            if (CanMove && _touchUpPosition != _touchDownPosition) _isTouchUp = true;
        }
        
        #endregion
    }
}