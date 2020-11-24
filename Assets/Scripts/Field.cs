using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class Field : MonoBehaviour
{
    [SerializeField] private Rigidbody2D[] _balls = default;
    
    public Action<List<IBall>> OnBallsStopped;
    
    private bool _isCheckingBallsMovement;
    private bool _areAllBallsStopped;
    
    private readonly List<IBall> _rolledBallsTypes = new List<IBall>();

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
                foreach (var ball in _balls)
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
}