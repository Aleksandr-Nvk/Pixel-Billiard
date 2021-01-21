using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using FieldData;
using System;
using Balls;

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
        
        private Vector3 _currentCuePosition;
        private Vector3 _currentCuePeakRotation;
        
        private float _currentForce;

        private Coroutine _currentFadeAnimation;
        
        private Action<List<Ball>> _onBallsStopped;
        private Action<Vector3> _onTouchDrag;
        
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
            _onTouchDrag = inputManager.OnTouchDrag += position =>
            {
                Move(position);
                Rotate(position);
            };
            inputManager.OnTouchUp += Hit;

            _onBallsStopped = _field.OnBallsStopped += _ => AlignWithWhiteBall();

            _whiteBall.OnReset += AlignWithWhiteBall;
        }

        private void OnDestroy()
        {
            _inputManager.OnTouchDown -= SetTouchDownData;
            _inputManager.OnTouchDrag -= _onTouchDrag;
            _inputManager.OnTouchUp -= Hit;

            _field.OnBallsStopped -= _onBallsStopped;
            _whiteBall.OnReset -= AlignWithWhiteBall;
        }
        
        private void Rotate(Vector3 touchDragPosition)
        {
            var cachedTransform = _cuePeak.transform;
            cachedTransform.LookAt(touchDragPosition, Vector3.back);
        
            _currentCuePeakRotation.z = cachedTransform.eulerAngles.z;
            _cuePeak.transform.eulerAngles = -_currentCuePeakRotation;
        }
        
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
        
        private void AlignWithWhiteBall()
        {
            _cuePeak.position = _whiteBall.gameObject.transform.position;
            _cuePeak.rotation = Quaternion.identity;

            gameObject.SetActive(true);
            
            StopCoroutine(_currentFadeAnimation);
            _currentFadeAnimation =
                StartCoroutine(_animations.Fade(_cueRenderer, targetAlpha: 1f, duration: 0.25f));
        }
        
        /// <summary>
        /// Runs hit animation and adds force to a white ball
        /// </summary>
        private void Hit(Vector3 touchUpPosition)
        {
            gameObject.SetActive(true);
            StartCoroutine(Hit());
            
            IEnumerator Hit()
            {
                yield return _animations.Move(_cue, Vector3.zero, duration: 0.025f, isLocal: true);
                
                var direction = -(touchUpPosition - _cuePeak.position).normalized;
                yield return new WaitForFixedUpdate();
                _whiteBall.Hit(force: _currentForce * direction, ForceMode2D.Impulse);
                
                _field.CheckBallsMovement();
                
                yield return _currentFadeAnimation =
                    StartCoroutine(_animations.Fade(_cueRenderer, targetAlpha: 0f, duration: 0.25f));
                gameObject.SetActive(false);
            }
        }
        
        private void SetTouchDownData(Vector3 touchPosition) => _touchDownPosition = touchPosition;
    }
}