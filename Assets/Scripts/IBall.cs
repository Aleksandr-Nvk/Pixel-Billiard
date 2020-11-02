public interface IBall
{
    BallType BallType { get; }
    
    /// <summary>
    /// Switches on the rolling animation
    /// </summary>
    void Roll();
}