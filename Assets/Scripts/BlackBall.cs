using UnityEngine;

public class BlackBall : MonoBehaviour, IBall
{
    [SerializeField] private Sprite _icon = default;
    
    public Sprite Icon => _icon;

    public void Roll()
    {
        gameObject.SetActive(false);
    }
}