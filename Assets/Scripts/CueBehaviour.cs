using UnityEngine;

public class CueBehaviour : MonoBehaviour
{
#pragma warning disable 0649

    [Header("Settings")]
    
    [SerializeField] private float _force;
    
    [SerializeField] private float _minForce;
    [SerializeField] private float _maxForce;

    [SerializeField] private Vector3 _startCueOffset;
    
    [SerializeField] private float _minCueOffset;
    [SerializeField] private float _maxCueOffset;

    [Header("Data")]
    
    [SerializeField] private Controls _controls;
    
    [SerializeField] private Rigidbody2D _whiteBall;
    
    [SerializeField] private GameObject _cuePeak;

    [SerializeField] private SpriteRenderer _cue;

#pragma warning restore

    private bool _isTouchUp;

    private Vector3 _touchDownPosition;
    private Vector3 _touchDragPosition;
    private Vector3 _touchUpPosition;
    
    private Vector3 _newCuePosition;
    private Vector3 _newCueRotation;
    
    private void Start()
    {
        _newCuePosition = transform.localPosition;
        _newCueRotation = _cuePeak.transform.eulerAngles;
        transform.localPosition += _startCueOffset;
        
        _controls.OnTouchDown += SetTouchDownData;
        _controls.OnTouchDrag += SetTouchDragData;
        _controls.OnTouchDrag += Move;
        _controls.OnTouchDrag += Rotate;
        _controls.OnTouchUp += SetTouchUpData;
    }

    private void FixedUpdate()
    {
        if (_isTouchUp)
        {
            Pull();
            _isTouchUp = false;
        }
    }

    private void Update()
    {
#if UNITY_EDITOR

        if (Input.GetKeyDown(KeyCode.A))
        {
            AlignWithWhiteBall();
        }

#endif
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
        
        var offset = touchDownRadius - touchDragRadius;
        
        _newCuePosition.y = Mathf.Clamp(offset, _minCueOffset, _maxCueOffset);
        transform.localPosition = _newCuePosition;
    }
    
    /// <summary>
    /// Sets cue peak gameObject position to the white ball position
    /// </summary>
    private void AlignWithWhiteBall()
    {
        _cuePeak.transform.position = _whiteBall.gameObject.transform.position;
        _cuePeak.transform.rotation = _whiteBall.gameObject.transform.rotation;
        
        transform.localPosition = Vector3.zero + _startCueOffset; // temp
    }
    
    /// <summary>
    /// Pulls the white ball with specific force to the specific direction
    /// </summary>
    private void Pull()
    {
        var force = Vector3.Distance(_touchUpPosition, _touchDownPosition) * _force * Time.fixedDeltaTime;
        var direction = -(_touchUpPosition - _whiteBall.gameObject.transform.position).normalized;
        
        _whiteBall.AddForce(direction * Mathf.Clamp(force, _minForce, _maxForce), ForceMode2D.Impulse);

        Animations.Move(transform, Vector3.zero, 0.025f, true);
    }

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
}