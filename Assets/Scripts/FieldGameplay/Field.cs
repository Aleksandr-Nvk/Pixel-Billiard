using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using Balls;

namespace FieldGameplay
{
    public class Field : MonoBehaviour
    {
        public Action<List<Ball>> OnBallsStopped;

        private readonly List<Ball> _allBalls = new List<Ball>();
        private readonly List<Ball> _rolledBalls = new List<Ball>();
        
        private bool _isCheckingBallsMovement;
        private bool _areAllBallsStopped;

        /// <summary>
        /// Checks if the balls move
        /// </summary>
        public void CheckBallsMovement()
        {
            if (!_isCheckingBallsMovement)
                StartCoroutine(Check());

            IEnumerator Check()
            {
                _isCheckingBallsMovement = true;

                do
                {
                    foreach (var ball in _allBalls)
                    {
                        if (ball.IsStopped)
                        {
                            _areAllBallsStopped = true;
                        }
                        else
                        {
                            _areAllBallsStopped = false;
                            break;
                        }
                    }
                
                    yield return new WaitForFixedUpdate();
                
                } while (!_areAllBallsStopped);

                OnBallsStopped?.Invoke(_rolledBalls);
                _rolledBalls.Clear();
            
                _isCheckingBallsMovement = false;
            }
        }

        /// <summary>
        /// Adds a new ball to ROLLED balls list
        /// </summary>
        /// <param name="rolledBall"> ROLLED ball </param>
        public void AddRolledBall(Ball rolledBall)
        {
            _rolledBalls.Add(rolledBall);
        }

        /// <summary>
        /// Adds a new ball to ALL balls list
        /// </summary>
        /// <param name="ball"> New ball </param>
        public void AddBall(Ball ball)
        {
            _allBalls.Add(ball);
        }
    }
}