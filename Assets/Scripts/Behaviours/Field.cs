using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;
using Bases;

namespace Behaviours
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private Ball[] _ballsEntities = default;
        
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
                    foreach (var ball in _ballsEntities)
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
        /// Adds a new ball type to rolled ball types list
        /// </summary>
        /// <param name="rolledBallType"> Rolled ball type </param>
        public void AddRolledBallType(Ball rolledBallType)
        {
            _rolledBalls.Add(rolledBallType);
        }

        /// <summary>
        /// Resets all balls' positions and activities
        /// </summary>
        public void ResetBalls()
        {
            foreach (var ballRigidBody in _ballsEntities)
            {
                ballRigidBody.Reset();
            }
        }
    }
}