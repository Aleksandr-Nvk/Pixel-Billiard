using UnityEngine;

namespace Balls
{
    public abstract class Ball : MonoBehaviour
    {
        [SerializeField] protected Rigidbody2D _rigidbody = default;

        public bool IsStopped => _rigidbody.velocity.sqrMagnitude < 0.0001f;
        
        protected AudioManager _audioManager;

        private Vector2 _startPosition;
        private Quaternion _startRotation;
        
        public void Init(AudioManager audioManager)
        {
            _audioManager = audioManager;
            
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.CompareTag("Field"))
                _audioManager.PlayBallWoodHitSound();
            
            else if (collision.gameObject.CompareTag("Ball"))
                _audioManager.PlayBallHitSound();
        }

        public virtual void Roll()
        {
            gameObject.SetActive(false);
        }
        
        public virtual void ResetBall()
        {
            _rigidbody.velocity = Vector2.zero;
            transform.position = _startPosition;
            transform.rotation = _startRotation;
            
            gameObject.SetActive(true);
        }
    }
}