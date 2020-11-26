using UnityEngine;

public class BlackBall : MonoBehaviour, IBall
{
    public void Roll()
    {
        gameObject.SetActive(false);
    }
}