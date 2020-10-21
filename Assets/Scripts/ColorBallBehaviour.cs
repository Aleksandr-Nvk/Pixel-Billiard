using UnityEngine;

public class ColorBallBehaviour : MonoBehaviour, IBall
{
#pragma warning disable 0649
    
    [SerializeField] private Ball _ballModel;

#pragma warning restore
    
    public Ball BallModel => _ballModel;
    
    public void Roll()
    {
        // rolling animation
        Destroy(gameObject);
    }
}