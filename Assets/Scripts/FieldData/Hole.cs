using UnityEngine;
using Balls;

namespace FieldData
{
    public class Hole : MonoBehaviour
    {
        [SerializeField] private Field _field = default;

        private void OnTriggerEnter2D(Collider2D ballCollider)
        {
            var ball = ballCollider.gameObject.GetComponent<Ball>();
        
            _field.AddRolledBall(ball);
            ball.Roll();
        
            Debug.Log($"{ballCollider.gameObject.name} rolled!");
        }
    }
}