using UnityEngine;

public class Hole : MonoBehaviour
{
    [SerializeField] private Field _field = default;
    
    private void OnTriggerEnter2D(Collider2D ballCollider)
    {
        var ball = ballCollider.gameObject.GetComponent<IBall>();
        
        if (ball is BlackBall)
            Debug.Log("GAME OVER");
        else
            _field.AddRolledBallType(ball);
        
        ball.Roll();
        
        Debug.Log($"{ballCollider.gameObject.name} rolled!");
    }
}