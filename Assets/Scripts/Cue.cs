using UnityEngine;

public class Cue : MonoBehaviour
{
    [Header("Settings")]
    
    [Range(0f, 30f)]
    [SerializeField] private float _force = default;

    [Range(0f, 30f)]
    [SerializeField] private float _sensitivity = default;

    [Range(-5f, 5f)]
    [SerializeField] private float _minCueOffset = default;
    [Range(-5f, 5f)]
    [SerializeField] private float _maxCueOffset = default;
    
    [Header("Data")]
    
    [SerializeField] private Controls _controls = default;

    [SerializeField] private Field _field = default;
    
    [SerializeField] private WhiteBall _whiteBall = default;
    
    [SerializeField] private GameObject _cuePeak = default;

    [SerializeField] private SpriteRenderer _cue = default;
    
    private bool _isTouchUp;

    private Vector3 _touchDownPosition;
    private Vector3 _touchDragPosition;
    private Vector3 _touchUpPosition;
    
    private Vector3 _newCuePosition;
    private Vector3 _newCueRotation;
    
    private float _minForce;
    private float _maxForce;
    
    private void Start()
    {
        _newCuePosition = transform.localPosition;
        _newCueRotation = _cuePeak.transform.eulerAngles;
        transform.position = _whiteBall.gameObject.transform.position;

        _minForce = _minCueOffset * _force;
        _maxForce = _maxCueOffset * _force;
        
        _controls.OnTouchDown += SetTouchDownData;
        _controls.OnTouchDrag += SetTouchDragData;
        _controls.OnTouchDrag += Move;
        _controls.OnTouchDrag += Rotate;
        _controls.OnTouchUp += SetTouchUpData;

        _field.OnBallsStopped += _ => { AlignWithWhiteBall(); };
    }

    private void FixedUpdate()
    {
        if (_isTouchUp)
        {
            Pull();
            _isTouchUp = false;
        }
    }

    private void OnDestroy()
    {
        _controls.OnTouchDown -= SetTouchDownData;
        _controls.OnTouchDrag -= SetTouchDragData;
        _controls.OnTouchDrag -= Move;
        _controls.OnTouchDrag -= Rotate;
        _controls.OnTouchUp -= SetTouchUpData;
    }

    /// <summary>
    /// Rotates the cue depending on touch position
    /// </summary>
    private void Rotate(Vector3 touchDragPosition)
    {
        var tempTransform = _cuePeak.transform;
        tempTransform.LookAt(touchDragPosition, Vector3.back);
        
        _newCueRotation.z = tempTransform.eulerAngles.z;
        _cuePeak.transform.eulerAngles = -_newCueRotation;
    }

    /// <summary>
    /// Moves the cue depending on touch position
    /// </summary>
    private void Move(Vector3 touchDragPosition)
    {
        var touchDownRadius = Vector3.Distance(_whiteBall.gameObject.transform.position, _touchDownPosition);
        var touchDragRadius = Vector3.Distance(_whiteBall.gameObject.transform.position, touchDragPosition);
        
        var offset = (touchDownRadius - touchDragRadius) * _sensitivity;
        
        _newCuePosition.y = Mathf.Clamp(offset, -_maxCueOffset, 0);
        transform.localPosition = _newCuePosition;
    }
    
    /// <summary>
    /// Sets cue position to the white ball position
    /// </summary>
    private void AlignWithWhiteBall()
    {
        _cuePeak.transform.position = _whiteBall.gameObject.transform.position;
        _cuePeak.transform.rotation = _whiteBall.gameObject.transform.rotation;
        
        Animations.Fade(_cue, 1f, 0.5f);
    }
    
    /// <summary>
    /// Pulls the white ball with specific force to the specific direction
    /// </summary>
    private void Pull()
    {
        var force = Vector3.Distance(_touchUpPosition, _touchDownPosition) * _force;
        var direction = -(_touchUpPosition - _whiteBall.gameObject.transform.position).normalized;

        Animations.Move(transform, Vector3.zero, 0.025f, true);
        Animations.Fade(_cue, 0f, 0.5f);
        _whiteBall.Hit(direction * Mathf.Clamp(force, _minForce, _maxForce), ForceMode2D.Impulse);
    }

    #region SetTouchData
    
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

        _isTouchUp = true;
    }
    
    #endregion
}