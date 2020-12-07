using UnityEngine;
using System;
using Balls;

namespace FieldGameplay
{
    public class Player
    {
        public bool HasStripedBalls;

        public string Name { get; private set; }

        public int RolledColorBallsCount { get; private set; }

        public Action<Sprite> OnBallRolled;

        public Player(string name)
        {
            Name = name;
        }
        
        /// <summary>
        /// Adds the rolled ball to a list
        /// </summary>
        /// <param name="rolledBall"> Rolled color ball</param>
        public void AddRolledBall(ColorBall rolledBall)
        {
            RolledColorBallsCount++;
            OnBallRolled?.Invoke(rolledBall.Icon);
        }
        
        /// <summary>
        /// Resets player's data
        /// </summary>
        public void Reset()
        {
            HasStripedBalls = default;
            Name = default;
            RolledColorBallsCount = default;
        }
    }
}