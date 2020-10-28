using System.Collections;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private static Animations _instance;

    private static Vector3 _velocity = Vector3.zero;
    
    private void Awake()
    {
        _instance = this;
    }

    /// <summary>
    /// Smoothly moves the transform to the target position 
    /// </summary>
    /// <param name="transform"> Transform to move </param>
    /// <param name="targetPosition"> Position to move to </param>
    /// <param name="duration"> Animation duration in seconds </param>
    /// <param name="isLocal"> True if movements are applied in local coordinates </param>
    /// <returns> Animation </returns>
    public static Coroutine Move(Transform transform, Vector3 targetPosition, float duration, bool isLocal = false)
    {
        var progress = 0f;
        
        IEnumerator Move()
        {
            while (progress < duration)
            {
                if (!isLocal)
                    transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref _velocity, duration);
                else
                    transform.localPosition = Vector3.SmoothDamp(transform.localPosition, targetPosition, ref _velocity, duration);

                progress += Time.deltaTime;
                
                yield return null;
            }

            progress = 0f;
        }

        var animation = _instance.StartCoroutine(Move());
        return animation;
    }

    /// <summary>
    /// Stops the animation
    /// </summary>
    /// <param name="animation"> Animation to stop </param>
    public static void Stop(Coroutine animation)
    {
        _instance.StopCoroutine(animation);
    }
}