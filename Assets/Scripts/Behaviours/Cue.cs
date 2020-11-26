using System.Collections.Generic;
using UnityEngine;
using System;

public class Cue : MonoBehaviour
{
    [Header("Settings")]
    
    [Range(0f, 30f)]
    [SerializeField] private float _forceCoefficient = default;

    [Range(0f, 30f)]
    [SerializeField] private float _sensitivity = default;

    [Range(-5f, 5f)]
    [SerializeField] private float _maxCueOffset = default;
    
    [Header("Data")]
    
    [SerializeField] private GameObject _cuePeak = default;

    [SerializeField] private Controls _controls = default;

    [SerializeField] private Field _field = default;
    
    [SerializeField] private WhiteBall _whiteBall = default;
    
    [SerializeField] private SpriteRenderer _cue = default;
    
    private bool _canMove = true;
    private bool _isTouchUp;
    
    private Vector3 _touchDownPosition;
    private Vector3 _touchDragPosition;
    private Vector3 _touchUpPosition;
    
    private Vector3 _newCuePosition;
    private Vector3 _newCueRotation;

    private Coroutine _cueFadeAnimation;

    private Action<List<IBall>> _onBallsStopped;
    
    private void Start()
    {
        _newCuePosition = transform.localPosition;
        _newCueRotation = _cuePeak.transform.eulerAngles;
        transform.position = _whiteBall.gameObject.transform.position;

        _controls.OnTouchDown += SetTouchDownData;
        _controls.OnTouchDrag += SetTouchDragData;
        _controls.OnTouchDrag += Move;
        _controls.OnTouchDrag += Rotate;
        _controls.OnTouchUp += SetTouchUpData;

        _onBallsStopped = _field.OnBallsStopped += _ =>
        {
            AlignWithWhiteBall();
            _canMove = true;
        };

        _whiteBall.OnRespawned += AlignWithWhiteBall;
    }

    private void FixedUpdate()
    {
        if (_isTouchUp && _canMove)
        {
            Pull();
            
            _isTouchUp = false;
            _canMove = false;
        }
    }

    private void OnDestroy()
    {
        _controls.OnTouchDown -= SetTouchDownData;
        _controls.OnTouchDrag -= SetTouchDragData;
        _controls.OnTouchDrag -= Move;
        _controls.OnTouchDrag -= Rotate;
        _controls.OnTouchUp -= SetTouchUpData;

        _field.OnBallsStopped -= _onBallsStopped;
        
        _whiteBall.OnRespawned -= AlignWithWhiteBall;
    }

    /// <summary>
    /// Rotates the cue depending on touch position
    /// </summary>
    private void Rotate(Vector3 touchDragPosition)
    {
        if (_canMove)
        {
            var tempTransform = _cuePeak.transform;
            tempTransform.LookAt(touchDragPosition, Vector3.back);
        
            _newCueRotation.z = tempTransform.eulerAngles.z;
            _cuePeak.transform.eulerAngles = -_newCueRotation;
        }
    }

    /// <summary>
    /// Moves the cue depending on touch position
    /// </summary>
    private void Move(Vector3 touchDragPosition)
    {
        if (_canMove)
        {
            var touchDownRadius = Vector2.Distance(_whiteBall.gameObject.transform.position, _touchDownPosition);
            var touchDragRadius = Vector2.Distance(_whiteBall.gameObject.transform.position, touchDragPosition);
        
            var offset = (touchDownRadius - touchDragRadius) * _sensitivity;
        
            _newCuePosition.y = Mathf.Clamp(offset, -_maxCueOffset, 0);
            transform.localPosition = _newCuePosition;
        }
    }
    
    /// <summary>
    /// Sets cue position to the white ball position
    /// </summary>
    private void AlignWithWhiteBall()
    {
        _cuePeak.transform.position = _whiteBall.gameObject.transform.position;
        _cuePeak.transform.rotation = _whiteBall.gameObject.transform.rotation;
        
        Animations.Stop(_cueFadeAnimation);
        Animations.Fade(_cue, 1f, 0.5f);
    }
    
    /// <summary>
    /// Pulls the white ball with specific force to the specific direction
    /// </summary>
    private void Pull()
    {
        Vector2 direction = (-(_touchUpPosition - _whiteBall.gameObject.transform.position)).normalized;

        var force = Vector2.Distance(_cuePeak.transform.position,
            _cue.transform.TransformPoint(_cue.transform.localPosition));
        
        Animations.Move(transform, Vector3.zero, 0.025f, true);
        _cueFadeAnimation = Animations.Fade(_cue, 0f, 0.5f);
        
        _whiteBall.Hit(_forceCoefficient * force * direction, ForceMode2D.Impulse);
    }

    #region Input
    
    private void SetTouchDownData(Vector3 touchPosition)
    {
        _touchDownPosition = touchPosition;
    }
    private void SetTouchDragData(Vector3 touchPosition)
    {
        _touchDragPosition = touchPosition;
    }
    private void SetTouchUpData(Vector3 touchPosition)
    {
        _touchUpPosition = touchPosition;

        if (_canMove) _isTouchUp = true;
    }
    
    #endregion
}