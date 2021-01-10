using UnityEngine;
using System;
using Balls;

namespace FieldData
{
    public class Player
    {
        public string Name { get; private set; }

        public bool HasStripedBalls;
        
        public int RolledColorBallsCount { get; private set; }

        public Action<Sprite> OnBallRolled;

        public Player(string name)
        {
            Name = name;
        }
        
        public void AddRolledBall(ColorBall rolledBall)
        {
            RolledColorBallsCount++;
            OnBallRolled?.Invoke(rolledBall.Icon);
        }
    }
}