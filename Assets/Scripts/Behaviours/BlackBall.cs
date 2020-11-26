using UnityEngine;
using Interfaces;

namespace Behaviours
{
    public class BlackBall : MonoBehaviour, IBall
    {
        private Vector2 _startPosition;
        private Quaternion _startRotation;
        
        private void Start()
        {
            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        public void Roll()
        {
            gameObject.SetActive(false);
        }

        public void Reset()
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
            gameObject.SetActive(true);
        }
    }
}