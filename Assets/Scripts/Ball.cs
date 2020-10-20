using UnityEngine;

public class Ball : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Hole"))
        {
            Debug.Log($"{name} rolled!");
            Destroy(gameObject);
        }
    }
}