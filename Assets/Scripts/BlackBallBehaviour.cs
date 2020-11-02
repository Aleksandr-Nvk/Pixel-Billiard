using UnityEngine;

public class BlackBallBehaviour : MonoBehaviour, IBall
{
#pragma warning disable 0649
    
    [SerializeField] private Sprite _icon;

    [SerializeField] private BallType _ballType;

#pragma warning restore
    
    public BallType BallType => _ballType;

    public Sprite Icon => _icon;
    
    public void Roll()
    {
        // rolling animation
        Destroy(gameObject);
    }
}