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

        private Ball[] _allBalls;
        
        private readonly List<Ball> _rolledBalls = new List<Ball>();

        private WhiteBall _whiteBall;
        
        private bool _isCheckingBallsMovement;
        private bool _areAllBallsStopped;

        public void Init(Ball[] allBalls, WhiteBall whiteBall)
        {
            _allBalls = allBalls;
            _whiteBall = whiteBall;
        }
        
        private void ResetWhiteBall()
        {
            if (_whiteBall.IsRolled)
                _whiteBall.ResetBall();
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
                ResetWhiteBall();
                _rolledBalls.Clear();
            
                _isCheckingBallsMovement = false;
            }
        }
        
        /// <summary>
        /// Adds a new ball to rolled balls list
        /// </summary>
        /// <param name="rolledBall"> Rolled ball </param>
        public void AddRolledBall(Ball rolledBall)
        {
            _rolledBalls.Add(rolledBall);
        }
    }
}