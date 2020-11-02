using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System;

public class MoveManager : MonoBehaviour
{
#pragma warning disable 0649
    
    [SerializeField] private Rigidbody2D[] _balls;

    [SerializeField] private HoleBehaviour[] _holes;
    
#pragma warning restore

    private Player _firstPlayer;
    private Player _secondPlayer;
    
    private Player _currentPlayer;

    private int _rolledColorBallsCount;
    
    private bool _isCheckingBallsMovement;
    private bool _hasToSwitch;

    public void Init(Player firstPlayer, Player secondPlayer)
    {
        _firstPlayer = firstPlayer;
        _secondPlayer = secondPlayer;
    }
    
    private void Start()
    {
        _currentPlayer = _firstPlayer;

        foreach (var hole in _holes)
        {
            hole.OnBallRolled += HoleHandler;
        }
    }

    private void OnDestroy()
    {
        foreach (var hole in _holes)
        {
            hole.OnBallRolled -= HoleHandler;
        }
    }

    /// <summary>
    /// Process the rolled balls
    /// </summary>
    /// <param name="rolledBallType"> Rolled ball type </param>
    private void HoleHandler(BallType rolledBallType)
    {
        if (rolledBallType == BallType.Black)
        {
            // GameOver();
            Debug.Log("GAME OVER");
        }
        else
        {
            if (!_isCheckingBallsMovement)
                StartCoroutine(Check());
        }
        
        if (_rolledColorBallsCount == 0 & rolledBallType != BallType.White & rolledBallType != BallType.Black)
        {
            SetPlayersBallType(rolledBallType);
            _rolledColorBallsCount++;
        }
        if ((rolledBallType != _currentPlayer.BallType && rolledBallType != BallType.Black) | rolledBallType == BallType.White)
            _hasToSwitch = true;
    }

    /// <summary>
    /// Checks if all balls don't move
    /// </summary>
    private IEnumerator Check()
    {
        _isCheckingBallsMovement = true;
        
        var stoppedBalls = new List<Rigidbody2D>();
        
        while (stoppedBalls.Count != _balls.Length)
        {
            foreach (var ball in _balls)
            {
                if (Math.Abs(ball.velocity.magnitude) < 0.01f && !stoppedBalls.Contains(ball)) 
                    stoppedBalls.Add(ball);
            }
            
            yield return new WaitForFixedUpdate();
        }

        if (_hasToSwitch)
            SwitchPlayer();

        _isCheckingBallsMovement = false;
    }

    /// <summary>
    /// Switches the current player to the next one
    /// </summary>
    private void SwitchPlayer()
    {
        _currentPlayer = _currentPlayer == _firstPlayer ?
            _secondPlayer : 
            _firstPlayer;

        _hasToSwitch = false;
        Debug.Log($"Switched to {_currentPlayer.Name}");
    }

    /// <summary>
    /// Sets the players ball type
    /// </summary>
    /// <param name="ballType"> First color ball to be rolled </param>
    private void SetPlayersBallType(BallType ballType)
    {
        _currentPlayer.BallType = ballType;

        if (_firstPlayer == _currentPlayer)
        {
            _secondPlayer.BallType = ballType == BallType.ColorFilled ?
                BallType.ColorStripped : 
                BallType.ColorFilled;
        }
        else
        {
            _firstPlayer.BallType = ballType == BallType.ColorFilled ?
                BallType.ColorStripped : 
                BallType.ColorFilled;
        }
        Debug.Log($"{_firstPlayer.Name}: {_firstPlayer.BallType}, {_secondPlayer.Name}: {_secondPlayer.BallType}");
    }
}