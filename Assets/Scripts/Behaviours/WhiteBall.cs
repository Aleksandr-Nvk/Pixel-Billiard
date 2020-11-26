using System.Collections.Generic;
using UnityEngine;
using Interfaces;
using Models;
using System;

namespace Behaviours
{
    public class WhiteBall : MonoBehaviour, IBall
    {
        [SerializeField] private Field _field = default;
    
        [SerializeField] private Rigidbody2D _ball = default;

        public Action OnReset;

        private Vector3 _startPosition;
        private Quaternion _startRotation;
        
        private Action<List<IBall>> _onBallsStoppedLambda;

        private bool _isRolled;
        
        private void Start()
        {
            _onBallsStoppedLambda = _field.OnBallsStopped += _ => { if (_isRolled) Reset(); };

            _startPosition = transform.position;
            _startRotation = transform.rotation;
        }

        private void OnDestroy()
        {
            _field.OnBallsStopped -= _onBallsStoppedLambda;
        }

        public void Roll()
        {
            _isRolled = true;

            gameObject.SetActive(false);
        }
        
        public void Reset()
        {
            transform.position = _startPosition;
            transform.rotation = _startRotation;
            _isRolled = false;
            gameObject.SetActive(true);
            
            OnReset?.Invoke();
        }

        /// <summary>
        /// Hits the white ball within physics
        /// </summary>
        /// <param name="force"> Hit force </param>
        /// <param name="forceMode"> Force mode </param>
        public void Hit(Vector3 force, ForceMode2D forceMode)
        {
            _ball.AddForce(force, forceMode);
            _field.CheckBallsMovement();
        }
    }
}