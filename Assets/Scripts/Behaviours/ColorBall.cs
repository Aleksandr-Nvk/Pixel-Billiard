using UnityEngine;

public class ColorBall : MonoBehaviour, IBall
{
    [SerializeField] private bool _isStriped = default;
    
    [SerializeField] private Sprite _icon = default;

    public bool IsStriped => _isStriped;
    
    public Sprite Icon => _icon;
    
    public void Roll()
    {
        gameObject.SetActive(false);
    }
}