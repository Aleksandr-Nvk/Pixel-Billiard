using UnityEngine;

public class BlackBall : MonoBehaviour, IBall
{
#pragma warning disable 0649
    
    [SerializeField] private BallType _ballType;
    
#pragma warning restore
    
    public BallType BallType => _ballType;
    
    public void Roll()
    {
        gameObject.SetActive(false);
    }
}