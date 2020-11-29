using System.Collections.Generic;
using UnityEngine;
using System;
using Bases;

namespace Behaviours
{
    public class WhiteBall : Ball
    {
        [SerializeField] private Field _field = default;
        
        public Action OnReset;
        
        private Action<List<Ball>> _onBallsStoppedLambda;

        private bool _isRolled;
        
        protected override void Start()
        {
            base.Start();
            
            _onBallsStoppedLambda = _field.OnBallsStopped += _ => { if (_isRolled) Reset(); };
        }

        private void OnDestroy()
        {
            _field.OnBallsStopped -= _onBallsStoppedLambda;
        }

        public override void Roll()
        {
            base.Roll();

            _isRolled = true;
        }

        public override void Reset()
        {
            base.Reset();
            
            _isRolled = false;
            
            OnReset?.Invoke();
        }

        /// <summary>
        /// Hits the white ball within physics
        /// </summary>
        /// <param name="force"> Hit force </param>
        /// <param name="forceMode"> Force mode </param>
        public void Hit(Vector3 force, ForceMode2D forceMode)
        {
            _rigidbody.AddForce(force, forceMode);
            _field.CheckBallsMovement();
        }
    }
}