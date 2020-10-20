using UnityEngine;

public class WhiteBall : MonoBehaviour
{
    private bool _isPressed = false;
    
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
            _isPressed = true;
    }

    private void FixedUpdate()
    {
        if (_isPressed)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)), ForceMode2D.Impulse);
            _isPressed = false;
        }
    }
}
