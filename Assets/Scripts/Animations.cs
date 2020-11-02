using System.Collections;
using UnityEngine;

public class Animations : MonoBehaviour
{
    private static Animations _instance;

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
        var _velocity = Vector3.zero;
        
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
        }

        return _instance.StartCoroutine(Move());
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