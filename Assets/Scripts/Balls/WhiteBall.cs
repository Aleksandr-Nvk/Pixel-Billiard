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

        public void Hit(Vector3 force, ForceMode2D forceMode)
        {
            _rigidbody.AddForce(force, forceMode);
            _audioManager.PlayBallHitSound();
        }
    }
}