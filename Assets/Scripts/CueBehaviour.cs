using UnityEngine;

public class CueBehaviour : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField] private Controls _controls;
    
    [SerializeField] private Rigidbody2D _whiteBall;

    [SerializeField] private SpriteRenderer _cue;
    
#pragma warning restore

    private bool _isTouchUp;

    private Vector2 _touchDownPosition;
    private Vector2 _touchDragPosition;
    private Vector2 _touchUpPosition;

    private void Start()
    {
        _controls.OnTouchDown += SetTouchDownData;
        _controls.OnTouchDrag += SetTouchDragData;
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

    /// <summary>
    /// Pulls the white ball with specific force to the specific direction
    /// </summary>
    private void Pull()
    {
        var force = Vector3.Distance(_touchUpPosition, _touchDownPosition) * 200f * Time.fixedDeltaTime;
        var direction = -(_touchUpPosition - _touchDownPosition).normalized;
        
        _whiteBall.AddForce(direction * force, ForceMode2D.Impulse);
    }
}