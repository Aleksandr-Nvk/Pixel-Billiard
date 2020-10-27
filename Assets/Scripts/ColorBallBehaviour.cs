using UnityEngine;

public class ColorBallBehaviour : MonoBehaviour, IBall
{
#pragma warning disable 0649
    
    [SerializeField] private Sprite _icon;

#pragma warning restore
    
    public Sprite Icon => _icon;
    
    public void Roll()
    {
        // rolling animation
        Destroy(gameObject);
    }
}