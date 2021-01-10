using UnityEngine;
using Balls;

namespace FieldData
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private Field _field = default;

        private void OnTriggerEnter2D(Collider2D ballCollision)
        {
            var ball = ballCollision.gameObject.GetComponent<Ball>();
        
            ball.Roll();
            _field.AddRolledBall(ball);
        }
    }
}