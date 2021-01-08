using System.Collections.Generic;
using System;
using Balls;
using Models;

namespace FieldData
{
    public class MoveManager
    {
        public Action<Player> OnPlayerSwitched;

        public Player CurrentPlayer { get; private set; }

        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;

        private readonly GameSession _gameSession;
        
        private bool _hasToSwitch;

        public MoveManager(Field field, Player firstPlayer, Player secondPlayer, GameSession gameSession)
        {
            CurrentPlayer = firstPlayer;
            _firstPlayer = firstPlayer;
            _secondPlayer = secondPlayer;

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
                        case BlackBall _ when CurrentPlayer.RolledColorBallsCount == 7: // black ball rolled (win)
                            _gameSession.End(CurrentPlayer);
                            break;
                    
                        case BlackBall _: // black ball rolled (lose)
                            var winner = CurrentPlayer == _firstPlayer 
                                ? _secondPlayer 
                                : _firstPlayer;
                            _gameSession.End(winner);
                            break;
                    
                        case ColorBall ball: // some color ball rolled
                        
                            // first color ball rolled
                            if (_firstPlayer.RolledColorBallsCount == 0 && _secondPlayer.RolledColorBallsCount == 0)
                                SetPlayersBallType(rolledBall);

                            if (ball.IsStriped == CurrentPlayer.HasStripedBalls) // right color ball type
                            {
                                CurrentPlayer.AddRolledBall(ball);
                            }
                            else // false color type (switch)
                            {
                                if (CurrentPlayer == _firstPlayer)
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
        }
    
        /// <summary>
        /// Switches the current player to the next one
        /// </summary>
        private void SwitchPlayer()
        {
            if (_hasToSwitch)
            {
                CurrentPlayer = CurrentPlayer == _firstPlayer
                    ? _secondPlayer
                    : _firstPlayer;
                
                OnPlayerSwitched?.Invoke(CurrentPlayer);
                
                _hasToSwitch = false;
            }
        }

        /// <summary>
        /// Sets the players color ball type
        /// </summary>
        /// <param name="ball"> First rolled color ball </param>
        private void SetPlayersBallType(Ball ball)
        {
            CurrentPlayer.HasStripedBalls = ((ColorBall) ball).IsStriped;

            var opponent = CurrentPlayer == _firstPlayer
                ? _secondPlayer
                : _firstPlayer;
            opponent.HasStripedBalls = !((ColorBall) ball).IsStriped;
        }
    }
}