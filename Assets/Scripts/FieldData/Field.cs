using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using Balls;

namespace FieldData
{
    public class Field : MonoBehaviour
    {
        public Action<List<Ball>> OnBallsStopped;
        
        private readonly List<Ball> _rolledBalls = new List<Ball>();
        private Ball[] _allBalls;

        private InputManager _inputManager;
        private WhiteBall _whiteBall;
        
        private bool _isCheckingBallsMovement;
        private bool _areAllBallsStopped;

        public void Init(Ball[] allBalls, WhiteBall whiteBall, InputManager inputManager)
        {
            _allBalls = allBalls;
            _whiteBall = whiteBall;
            _inputManager = inputManager;
        }

        /// <summary>
        /// Check whether all active balls are moving
        /// </summary>
        public void CheckBallsMovement()
        {
            _inputManager.StopChecking();
            
            if (!_isCheckingBallsMovement)
                StartCoroutine(Check());

            IEnumerator Check()
            {
                _isCheckingBallsMovement = true;
                bool hasRollingBalls;

                do
                {
                    hasRollingBalls = false;
                    
                    foreach (var ball in _allBalls)
                    {
                        if (ball.IsRolling)
                        {
                            hasRollingBalls = true;
                            break;
                        }
                    }

                    yield return new WaitForFixedUpdate();

                } while (hasRollingBalls);

                OnBallsStopped?.Invoke(_rolledBalls);
                _isCheckingBallsMovement = false;
                
                if (_whiteBall.IsRolled)
                    _whiteBall.ResetBall();
                
                _rolledBalls.Clear();
                _inputManager.StartChecking();
            }
        }
        
        public void AddRolledBall(Ball rolledBall)
        {
            _rolledBalls.Add(rolledBall);
        }
    }
}