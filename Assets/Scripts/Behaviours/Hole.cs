using UnityEngine;
using Interfaces;
using Models;

namespace Behaviours
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private Field _field = default;
    
        private void OnTriggerEnter2D(Collider2D ballCollider)
        {
            var ball = ballCollider.gameObject.GetComponent<IBall>();
        
            _field.AddRolledBallType(ball);
            ball.Roll();
        
            Debug.Log($"{ballCollider.gameObject.name} rolled!");
        }
    }
}