using UnityEngine;

public class CueBehaviour : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField] private Rigidbody2D _whiteBall;
    
#pragma warning restore
    
    private bool _isPressed = false;

    private Vector2 _touchPosition;
    
    private void Update()
    {
#if  UNITY_EDITOR
        
        if (Input.GetMouseButtonDown(0))
        {
            _isPressed = true;
            _touchPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }

#endif
        
        if (Input.touchCount > 0)
        {
            _isPressed = true;
            _touchPosition = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
        }
    }

    private void FixedUpdate()
    {
        if (_isPressed)
        {
            var targetPosition = (_touchPosition - new Vector2(_whiteBall.position.x, _whiteBall.position.y)).normalized;
            _whiteBall.AddForce(targetPosition * 10f, ForceMode2D.Impulse);
            
            _isPressed = false;
        }
    } 
}