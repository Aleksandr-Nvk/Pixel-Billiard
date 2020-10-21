public interface IBall
{
    Ball BallModel { get; } // ball data

    /// <summary>
    /// Switches on the rolling animation
    /// </summary>
    void Roll();
}