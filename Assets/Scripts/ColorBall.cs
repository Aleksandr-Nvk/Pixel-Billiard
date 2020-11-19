using UnityEngine;

public class ColorBall : MonoBehaviour, IBall
{
#pragma warning disable 0649
    
    [SerializeField] private Sprite _icon;
    
    [SerializeField] private BallType _ballType;
    
#pragma warning restore
    
    public BallType BallType => _ballType;
    
    public Sprite Icon => _icon;
    
    public void Roll()
    {
        gameObject.SetActive(false);
    }
}