using UnityEngine;

public class HoleBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var ball = other.gameObject.GetComponent<IBall>();
        
        if (ball is ColorBallBehaviour)
        {
            Debug.Log($"{other.gameObject.name} rolled!");
            ball.Roll();
        }
    }
}