using UnityEngine;
using System;

public class HoleBehaviour : MonoBehaviour
{
    public Action<BallType> OnBallRolled;
    
    private void OnTriggerEnter2D(Collider2D ballCollider)
    {
        var ball = ballCollider.gameObject.GetComponent<IBall>();
        
        OnBallRolled?.Invoke(ball.BallType);
        ball.Roll();
        
        Debug.Log($"{ballCollider.gameObject.name} rolled!");
    }
}