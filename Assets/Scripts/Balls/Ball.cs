using UnityEngine;

namespace Balls
{
    public abstract class Ball : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rigidbody = default;

        public bool IsStopped => _rigidbody.velocity.sqrMagnitude < 0.0001f;

        private Vector2 _startPosition;
        private Quaternion _startRotation;
        
        protected virtual void Start()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
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