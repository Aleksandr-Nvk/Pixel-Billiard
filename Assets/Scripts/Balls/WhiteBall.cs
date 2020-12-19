using UnityEngine;
using System;

namespace Balls
{
    public class WhiteBall : Ball
    {
        public Action OnReset;
        
        public bool IsRolled { get; private set; }
        
        public override void Roll()
        {
            base.Roll();

            IsRolled = true;
        }

        public override void ResetBall()
        {
            base.ResetBall();
            
            IsRolled = false;
            OnReset?.Invoke();
        }

        /// <summary>
        /// Hits the white ball within physics
        /// </summary>
        /// <param name="force"> Hit force </param>
        /// <param name="forceMode"> Force mode </param>
        public void Hit(Vector3 force, ForceMode2D forceMode)
        {
            _audioManager.PlayBallHitSound();
            _rigidbody.AddForce(force, forceMode);
        }
    }
}