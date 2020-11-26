using Behaviours;
using System;

namespace Models
{
    public class GameSession
    {
        public Action<Player> OnSessionEnded;
        
        private readonly Player _firstPlayer;
        private readonly Player _secondPlayer;

        private readonly Field _fieldEntity;
        
        public GameSession(Player firstPlayer, Player secondPlayer, Field fieldEntity)
        {
            _firstPlayer = firstPlayer;
            _secondPlayer = secondPlayer;
            
            _fieldEntity = fieldEntity;
        }

        /// <summary>
        /// Resets a session ***(replay button)***
        /// </summary>
        public void ResetSession()
        {
            _fieldEntity.ResetBalls();
            
            _firstPlayer.Reset();
            _secondPlayer.Reset();
        }
        
        /// <summary>
        /// Ends a session and provides info about winner ***(black ball was rolled)***
        /// </summary>
        /// <param name="winner"></param>
        public void EndSession(Player winner)
        {
            OnSessionEnded?.Invoke(winner);
        }
    }
}