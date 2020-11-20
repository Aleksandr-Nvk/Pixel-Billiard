using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
    [SerializeField] private Field _field = default;

    [SerializeField] private MoveView _moveView = default;
    
    private Player _firstPlayer;
    private Player _secondPlayer;
    
    private Player _currentPlayer;

    private bool _isFirstColorBallRolled;
    private bool _hasToSwitch;

    public void Init(Player firstPlayer, Player secondPlayer)
    {
        _firstPlayer = firstPlayer;
        _secondPlayer = secondPlayer;
    }
    
    private void Start()
    {
        _currentPlayer = _firstPlayer;
        
        _field.OnBallsStopped += Handle;
    }

   /// <summary>
   /// Process the rolled balls
   /// </summary>
   /// <param name="rolledBalls"> All rolled balls </param>
    private void Handle(List<IBall> rolledBalls)
    {
        if (rolledBalls.Count == 0) // none of the balls rolled
            _hasToSwitch = true;
        
        foreach (var rolledBall in rolledBalls)
        {
            if (rolledBall is ColorBall ball)
            {
                if (!_isFirstColorBallRolled) // first color ball rolled
                {
                    SetPlayersBallType(rolledBall);
                    _isFirstColorBallRolled = true;
                }

                RollColorBall(ball);
            }
            
            if (rolledBall is ColorBall colorBall && colorBall.IsStriped != _currentPlayer.HasStripedBalls 
                || rolledBall is WhiteBall) // none of color balls rolled
            {
                _hasToSwitch = true;
            }
        }
        
        if (_hasToSwitch)
            SwitchPlayer();
    }
    
    /// <summary>
    /// Switches the current player to the next one
    /// </summary>
    private void SwitchPlayer()
    {
        if (_hasToSwitch)
        {
            _currentPlayer = _currentPlayer == _firstPlayer
                ? _secondPlayer
                : _firstPlayer;

            _hasToSwitch = false;
            _moveView.SwitchPointer();
            Debug.Log($"Switched to {_currentPlayer.Name}");
        }
    }

    /// <summary>
    /// Sets the players color ball type
    /// </summary>
    /// <param name="ball"> First rolled color ball </param>
    private void SetPlayersBallType(IBall ball)
    {
        _currentPlayer.HasStripedBalls = ((ColorBall)ball).IsStriped;

        var opponent = _currentPlayer == _firstPlayer
            ? _secondPlayer
            : _firstPlayer;
        opponent.HasStripedBalls = !((ColorBall)ball).IsStriped;

        Debug.Log($"{_firstPlayer.Name}: is striped: {_firstPlayer.HasStripedBalls}, " +
                  $"{_secondPlayer.Name}: is striped: {_secondPlayer.HasStripedBalls}");
    }

    /// <summary>
    /// Tells the player that his ball type was rolled
    /// </summary>
    /// <param name="rolledBall"> Rolled ball </param>
    private void RollColorBall(ColorBall rolledBall)
    {
        if (_firstPlayer.HasStripedBalls == rolledBall.IsStriped)
            _moveView.AddBallToFirstPlayer(rolledBall.Icon);
        else
            _moveView.AddBallToSecondPlayer(rolledBall.Icon);
    }
}