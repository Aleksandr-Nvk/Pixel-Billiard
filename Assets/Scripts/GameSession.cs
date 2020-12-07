using FieldGameplay;
using System;

public class GameSession
{
    public Action<Player> OnSessionEnded;

    /// <summary>
    /// Ends a session and provides info about winner
    /// </summary>
    /// <param name="winner"> Winner </param>
    public void EndSession(Player winner)
    {
        OnSessionEnded?.Invoke(winner);
    }
}