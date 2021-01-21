using UnityEngine;
using System;
using Balls;

namespace FieldData
{
    public class Player
    {
        public readonly string Name;
        
        public bool HasStripedBalls;
        
        public int RolledColorBallsCount { get; private set; }

        public Action<Sprite> OnBallAdded;

        public Player(string name)
        {
            Name = name;
        }
        
        public void AddRolledBall(ColorBall rolledBall)
        {
            RolledColorBallsCount++;
            OnBallAdded?.Invoke(rolledBall.Icon);
        }
    }
}