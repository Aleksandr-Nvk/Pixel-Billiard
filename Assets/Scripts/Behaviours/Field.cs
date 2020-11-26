using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using Interfaces;
using System;

namespace Behaviours
{
    public class Field : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D[] _ballsRigidbodies = default;
        
        public Action<List<IBall>> OnBallsStopped;
        
        private readonly List<IBall> _rolledBallsTypes = new List<IBall>();
        
        private readonly IBall[] _ballsEntities = new IBall[16]; // all existing balls on the scene
    
        private bool _isCheckingBallsMovement;
        private bool _areAllBallsStopped;

        private void Start()
        {
            // getting all IBall entities
            for (var i = 0; i < _ballsEntities.Length; i++)
            {
                _ballsEntities[i] = _ballsRigidbodies[i].gameObject.GetComponent<IBall>();
            }
        }

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
                    foreach (var ball in _ballsRigidbodies)
                    {
                        if (Math.Abs(ball.velocity.sqrMagnitude) < 0.0001f)
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

                OnBallsStopped?.Invoke(_rolledBallsTypes);
                _rolledBallsTypes.Clear();
            
                _isCheckingBallsMovement = false;
            }
        }

        /// <summary>
        /// Adds a new ball type to rolled ball types list
        /// </summary>
        /// <param name="rolledBallType"> Rolled ball type </param>
        public void AddRolledBallType(IBall rolledBallType)
        {
            _rolledBallsTypes.Add(rolledBallType);
        }

        /// <summary>
        /// Resets all balls' positions and activities
        /// </summary>
        public void ResetBalls()
        {
            foreach (var ballRigidBody in _ballsRigidbodies)
            {
                ballRigidBody.velocity = Vector2.zero;
            }
            
            foreach (var ballEntity in _ballsEntities)
            {
                ballEntity.Reset();
            }
        }
    }
}