using System.Collections.Generic;
using UnityEngine;

public class MoveManager : MonoBehaviour
{
#pragma warning disable 0649

    [SerializeField] private Field _field;
    
#pragma warning restore

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
   /// Process the rolled balls types
   /// </summary>
   /// <param name="rolledBallsTypes"> All rolled balls types </param>
    private void Handle(List<BallType> rolledBallsTypes)
    {
        if (rolledBallsTypes.Count == 0) // none of the balls rolled
            _hasToSwitch = true;
        
        foreach (var rolledBallType in rolledBallsTypes)
        {
            // first color ball rolled
            if (!_isFirstColorBallRolled && (rolledBallType == BallType.ColorFilled || rolledBallType == BallType.ColorStriped))
            {
                SetPlayersBallType(rolledBallType);
                _isFirstColorBallRolled = true;
            }
            
            if (rolledBallType != _currentPlayer.BallType || rolledBallType == BallType.White) // none of color balls rolled
                _hasToSwitch = true;
            
        }
        rolledBallsTypes.Clear();
        
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
            _currentPlayer = _currentPlayer == _firstPlayer ?
                _secondPlayer : 
                _firstPlayer;

            _hasToSwitch = false;
            Debug.Log($"Switched to {_currentPlayer.Name}");
        }
    }

    /// <summary>
    /// Sets the players ball type
    /// </summary>
    /// <param name="ballType"> First color ball to be rolled </param>
    private void SetPlayersBallType(BallType ballType)
    {
        _currentPlayer.BallType = ballType;

        var opponent = _currentPlayer == _firstPlayer
            ? _secondPlayer
            : _firstPlayer;

        opponent.BallType = ballType == BallType.ColorFilled
            ? BallType.ColorStriped
            : BallType.ColorFilled;

        Debug.Log($"{_firstPlayer.Name}: {_firstPlayer.BallType}, {_secondPlayer.Name}: {_secondPlayer.BallType}");
    }
}