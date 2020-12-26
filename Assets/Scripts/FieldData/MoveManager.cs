using System.Collections.Generic;
using UnityEngine;
using System;
using Balls;

namespace FieldData
{
    public class MoveManager
    {
        public Action<Player> OnPlayerSwitched;

        public Player FirstPlayer;
        public Player SecondPlayer;
    
        private Player _currentPlayer;
        
        private bool _hasToSwitch;

        public MoveManager(Field field)
        {
            _currentPlayer = FirstPlayer;
            
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
                            // End session
                            break;
                    
                        case BlackBall _: // black ball rolled (lose)
                            var winner = _currentPlayer == FirstPlayer 
                                ? FirstPlayer 
                                : SecondPlayer;
                            // End session
                            break;
                    
                        case ColorBall ball: // some color ball rolled
                        
                            // first color ball rolled
                            if (FirstPlayer.RolledColorBallsCount == 0 && SecondPlayer.RolledColorBallsCount == 0)
                                SetPlayersBallType(rolledBall);

                            if (ball.IsStriped == _currentPlayer.HasStripedBalls) // right color ball type
                            {
                                _currentPlayer.AddRolledBall(ball);
                            }
                            else // false color type (switch)
                            {
                                if (_currentPlayer == FirstPlayer)
                                    SecondPlayer.AddRolledBall(ball);
                                else
                                    FirstPlayer.AddRolledBall(ball);

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
        }
    
        /// <summary>
        /// Switches the current player to the next one
        /// </summary>
        private void SwitchPlayer()
        {
            if (_hasToSwitch)
            {
                _currentPlayer = _currentPlayer == FirstPlayer
                    ? SecondPlayer
                    : FirstPlayer;
                
                OnPlayerSwitched?.Invoke(_currentPlayer);
            
                Debug.Log($"Switched to {_currentPlayer.Name}");
                
                _hasToSwitch = false;
            }
        }

        /// <summary>
        /// Sets the players color ball type
        /// </summary>
        /// <param name="ball"> First rolled color ball </param>
        private void SetPlayersBallType(Ball ball)
        {
            _currentPlayer.HasStripedBalls = ((ColorBall)ball).IsStriped;

            var opponent = _currentPlayer == FirstPlayer
                ? SecondPlayer
                : FirstPlayer;
            opponent.HasStripedBalls = !((ColorBall)ball).IsStriped;

            Debug.Log($"{FirstPlayer.Name}: is striped: {FirstPlayer.HasStripedBalls}, " +
                      $"{SecondPlayer.Name}: is striped: {SecondPlayer.HasStripedBalls}");
        }
    }
}