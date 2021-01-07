using System;
using System.Collections;
using System.Collections.Generic;
using Balls;
using FieldData;
using UnityEngine;

namespace CueData
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

        private Animations _animations;
        private InputManager _inputManager;
        
        private WhiteBall _whiteBall;
        private Field _field;
        
        private Vector3 _touchDownPosition;
        private Vector3 _touchDragPosition;
        private Vector3 _touchUpPosition;
        
        private bool _isTouchUp;

        private Vector3 _currentCuePosition;
        private Vector3 _currentCuePeakRotation;
        
        private float _currentForce;

        private Coroutine _currentFadeAnimation;
        
        private Action<List<Ball>> _onBallsStopped;
        
        public void Init(WhiteBall whiteBall, Field field, InputManager inputManager, Animations animations)
        {
            _whiteBall = whiteBall;
            _field = field;

            _inputManager = inputManager;
            _animations = animations;

            _cuePeak.position = _whiteBall.gameObject.transform.position;
            _currentCuePosition = _cue.position;
            _currentCuePeakRotation = _cuePeak.eulerAngles;

            inputManager.OnTouchDown += SetTouchDownData;
            inputManager.OnTouchDrag += Move;
            inputManager.OnTouchDrag += Rotate;
            inputManager.OnTouchUp += SetTouchUpData;

            _whiteBall.OnReset += AlignWithWhiteBall;
            _onBallsStopped = _field.OnBallsStopped += _ => AlignWithWhiteBall();
        }

        private void FixedUpdate()
        {
            if (_isTouchUp)
            {
                Hit();
                _isTouchUp = false;
            }
        }

        private void OnDestroy()
        {
            _inputManager.OnTouchDown -= SetTouchDownData;
            _inputManager.OnTouchDrag -= Move;
            _inputManager.OnTouchDrag -= Rotate;
            _inputManager.OnTouchUp -= SetTouchUpData;

            _whiteBall.OnReset -= AlignWithWhiteBall;
            _field.OnBallsStopped -= _onBallsStopped;
        }

        /// <summary>
        /// Rotates the cue depending on touch position
        /// </summary>
        private void Rotate(Vector3 touchDragPosition)
        {
            var tempTransform = _cuePeak.transform;
            tempTransform.LookAt(touchDragPosition, Vector3.back);
        
            _currentCuePeakRotation.z = tempTransform.eulerAngles.z;
            _cuePeak.transform.eulerAngles = -_currentCuePeakRotation;
        }

        /// <summary>
        /// Moves the cue depending on touch position
        /// </summary>
        private void Move(Vector3 touchDragPosition)
        {
            var touchDownRadius = Vector2.Distance(_whiteBall.gameObject.transform.position, _touchDownPosition);
            var touchDragRadius = Vector2.Distance(_whiteBall.gameObject.transform.position, touchDragPosition);

            var offset = (touchDownRadius - touchDragRadius) * _sensitivity;
            var clampedOffset = Mathf.Clamp(offset, -_maxCueOffset, 0f);
            _currentCuePosition.y = clampedOffset;
            _cue.localPosition = _currentCuePosition;

            _currentForce = Mathf.Abs(clampedOffset) * _force;
        }
    
        /// <summary>
        /// Sets cue position to the white ball position
        /// </summary>
        private void AlignWithWhiteBall()
        {
            _cuePeak.position = _whiteBall.gameObject.transform.position;
            _cuePeak.rotation = Quaternion.identity;

            gameObject.SetActive(true);
            StopCoroutine(_currentFadeAnimation);
            _currentFadeAnimation = StartCoroutine(_animations.Fade(_cueRenderer, targetAlpha: 1f, duration: 0.25f));
        }
    
        /// <summary>
        /// Pulls the white ball with specific force to the specific direction
        /// </summary>
        private void Hit()
        {
            gameObject.SetActive(true);
            StartCoroutine(Hit());
            
            IEnumerator Hit()
            {
                yield return _animations.Move(_cue, Vector3.zero, duration: 0.025f, isLocal: true);
                
                Vector2 direction = (-(_touchUpPosition - _cuePeak.position)).normalized;
                _whiteBall.Hit(_currentForce * direction, ForceMode2D.Impulse);
                
                _field.CheckBallsMovement();
                
                yield return _currentFadeAnimation = StartCoroutine(_animations.Fade(_cueRenderer, targetAlpha: 0f, duration: 0.25f));
                gameObject.SetActive(false);
            }
        }

        #region Input
    
        private void SetTouchDownData(Vector3 touchPosition)
        {
            _touchDownPosition = touchPosition;
        }
        private void SetTouchUpData(Vector3 touchPosition)
        {
            _touchUpPosition = touchPosition;
            _isTouchUp = true;
        }
        
        #endregion
    }
}