using UnityEngine;

public class Hole : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField] private Field _field;
    
#pragma warning restore
    
    private void OnTriggerEnter2D(Collider2D ballCollider)
    {
        var ball = ballCollider.gameObject.GetComponent<IBall>();
        
        if (ball.BallType == BallType.Black)
            Debug.Log("GAME OVER");
        else
            _field.AddRolledBallType(ball.BallType);
        
        ball.Roll();
        
        Debug.Log($"{ballCollider.gameObject.name} rolled!");
    }
}