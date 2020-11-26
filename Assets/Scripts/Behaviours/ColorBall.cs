using UnityEngine;
using Interfaces;

namespace Behaviours
{
    public class ColorBall : MonoBehaviour, IBall
    {
        [SerializeField] private bool _isStriped = default;
    
        [SerializeField] private Sprite _icon = default;

        public bool IsStriped => _isStriped;
    
        public Sprite Icon => _icon;

        private Vector3 _startPosition;
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