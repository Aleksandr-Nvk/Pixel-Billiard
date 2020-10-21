using UnityEngine;

public class HoleBehaviour : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        var ball = other.gameObject.GetComponent<IBall>();
        
        if (ball.BallModel.BallType == BallType.Color)
        {
            Debug.Log($"{other.gameObject.name} rolled!");
            ball.Roll();
        }
    }
}