using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using Balls;

namespace FieldGameplay
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private Ball[] _allBalls = default;
        
        public Action<List<Ball>> OnBallsStopped;
        
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
        /// Adds a new ball type to rolled ball list
        /// </summary>
        /// <param name="rolledBall"> Rolled ball </param>
        public void AddRolledBallType(Ball rolledBall)
        {
            _rolledBalls.Add(rolledBall);
        }

        /// <summary>
        /// Resets all balls' positions and activities
        /// </summary>
        public void ResetBalls()
        {
            foreach (var ball in _allBalls)
            {
                ball.ResetBall();
            }
        }
    }
}