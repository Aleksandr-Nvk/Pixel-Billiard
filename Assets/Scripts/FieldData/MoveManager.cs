using System.Collections.Generic;
using System;
using Models;
using Balls;

namespace FieldData
{
    public class MoveManager
    {
        public Action<Player> OnPlayerSwitched;

        public Player CurrentPlayer { get; private set; }
        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;

        private readonly GameSession _gameSession;
        
        public MoveManager(Field field, Player firstPlayer, Player secondPlayer, GameSession gameSession)
        {
            CurrentPlayer = firstPlayer;
            _firstPlayer = firstPlayer;
            _secondPlayer = secondPlayer;

            _gameSession = gameSession;
            
            field.OnBallsStopped += Handle;
        }

        /// <summary>
        /// Handles the players depending on balls rolled during the last move
        /// </summary>
        /// <param name="rolledBalls"> Balls rolled during the last move </param>
        private void Handle(List<Ball> rolledBalls)
        {
            var hasToSwitch = false;
            
            if (rolledBalls.Count == 0) // none of the balls rolled
            {
                hasToSwitch = true;
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
                            var winner = GetOppositePlayer();
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
                                GetOppositePlayer().AddRolledBall(ball);
                                hasToSwitch = true;
                            }
                            
                            break;
                    
                        case WhiteBall _: // white ball rolled (switch)
                            hasToSwitch = true;
                            
                            break;
                    }
                }
            }
        
            if (hasToSwitch) SwitchPlayer();
        }
        
        private void SwitchPlayer()
        {
            CurrentPlayer = GetOppositePlayer();
            OnPlayerSwitched?.Invoke(CurrentPlayer);
        }

        private Player GetOppositePlayer()
        {
            return CurrentPlayer == _firstPlayer ? _secondPlayer : _firstPlayer;
        }

        /// <summary>
        /// Sets a ball type for each player
        /// </summary>
        /// <param name="ball"> The first ball rolled during session </param>
        private void SetPlayersBallType(Ball ball)
        {
            CurrentPlayer.HasStripedBalls = ((ColorBall) ball).IsStriped;

            var opponent = GetOppositePlayer();
            opponent.HasStripedBalls = !((ColorBall) ball).IsStriped;
        }
    }
}