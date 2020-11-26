using UnityEngine;
using System;

public class Player
{
    public bool HasStripedBalls;
    
    public readonly string Name;

    public int RolledColorBallsCount;
    
    public Action<Sprite> OnBallRolled;
    
    /// <summary>
    /// Adds the rolled ball to a list
    /// </summary>
    /// <param name="rolledBall"> Rolled color ball</param>
    public void AddRolledBall(ColorBall rolledBall)
    {
        RolledColorBallsCount++;
        OnBallRolled?.Invoke(rolledBall.Icon);
    }

    public Player(string name)
    {
        Name = name;
    }
}