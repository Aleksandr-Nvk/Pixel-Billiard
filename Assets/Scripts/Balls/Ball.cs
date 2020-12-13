using FieldGameplay;
using UnityEngine;

namespace Balls
{
    public abstract class Ball : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rigidbody = default;

        public bool IsStopped => _rigidbody.velocity.sqrMagnitude < 0.0001f;

        private Vector2 _startPosition;
        private Quaternion _startRotation;
        
        public virtual void Init(Field field)
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Field"))
            {
                //AudioManager.PlayBallWoodSound();
            }
            else if (collision.gameObject.CompareTag("Ball"))
            {
                //AudioManager.PlayBallSound();
            }
        }

        /// <summary>
        /// Rolls a ball
        /// </summary>
        public virtual void Roll()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Resets ball's data
        /// </summary>
        public virtual void ResetBall()
        {
            _rigidbody.velocity = Vector2.zero;
            
            transform.position = _startPosition;
            transform.rotation = _startRotation;
            gameObject.SetActive(true);
        }
    }
}