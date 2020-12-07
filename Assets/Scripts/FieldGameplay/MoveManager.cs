using System.Collections.Generic;
using UnityEngine;
using System;
using Balls;

namespace FieldGameplay
{
    public class MoveManager
    {
        public Action OnPlayerSwitched;

        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;
    
        private Player _currentPlayer;

        private readonly GameSession _gameSession;

        private bool _hasToSwitch;

        public MoveManager(GameSession gameSession, Field field, Player firstPlayer, Player secondPlayer)
        {
            _firstPlayer = firstPlayer;
            _secondPlayer = secondPlayer;
            _currentPlayer = _firstPlayer;

            _gameSession = gameSession;
        
            field.OnBallsStopped += Handle;
        }

        /// <summary>
        /// Process the rolled balls
        /// </summary>
        /// <param name="rolledBalls"> All rolled balls </param>
        private void Handle(List<Ball> rolledBalls)
        {
            if (rolledBalls.Count == 0) // none of the balls rolled
            {
                _hasToSwitch = true;
            }
            else // some ball rolled
            {
                foreach (var rolledBall in rolledBalls)
                {
                    switch (rolledBall)
                    {
                        case BlackBall _ when _currentPlayer.RolledColorBallsCount == 7: // black ball rolled (win)
                            _gameSession.EndSession(_currentPlayer);
                            break;
                    
                        case BlackBall _: // black ball rolled (lose)
                            var winner = _currentPlayer == _firstPlayer 
                                ? _firstPlayer 
                                : _secondPlayer;
                            _gameSession.EndSession(winner);
                            break;
                    
                        case ColorBall ball: // some color ball rolled
                        
                            // first color ball rolled
                            if (_firstPlayer.RolledColorBallsCount == 0 && _secondPlayer.RolledColorBallsCount == 0)
                                SetPlayersBallType(rolledBall);

                            if (ball.IsStriped == _currentPlayer.HasStripedBalls) // right color ball type
                            {
                                _currentPlayer.AddRolledBall(ball);
                            }
                            else // false color type (switch)
                            {
                                if (_currentPlayer == _firstPlayer)
                                    _secondPlayer.AddRolledBall(ball);
                                else
                                    _firstPlayer.AddRolledBall(ball);

                                _hasToSwitch = true;
                            }
                            break;
                    
                        case WhiteBall _: // white ball rolled (switch)
                            _hasToSwitch = true;
                            break;
                    }
                }
            }
        
            if (_hasToSwitch)
                SwitchPlayer();

            InputManager.StartTracking();
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
                OnPlayerSwitched?.Invoke();
            
                Debug.Log($"Switched to {_currentPlayer.Name}");
            }
        }

        /// <summary>
        /// Sets the players color ball type
        /// </summary>
        /// <param name="ball"> First rolled color ball </param>
        private void SetPlayersBallType(Ball ball)
        {
            _currentPlayer.HasStripedBalls = ((ColorBall)ball).IsStriped;

            var opponent = _currentPlayer == _firstPlayer
                ? _secondPlayer
                : _firstPlayer;
            opponent.HasStripedBalls = !((ColorBall)ball).IsStriped;

            Debug.Log($"{_firstPlayer.Name}: is striped: {_firstPlayer.HasStripedBalls}, " +
                      $"{_secondPlayer.Name}: is striped: {_secondPlayer.HasStripedBalls}");
        }
    }
}