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
    /// Smoothly moves the transform to the target position over time
    /// </summary>
    /// <param name="transform"> Transform to move </param>
    /// <param name="targetPosition"> Target position </param>
    /// <param name="duration"> Animation duration in seconds </param>
    /// <param name="isLocal"> Set TRUE if local transform movement required. False by default </param>
    /// <returns> Animation </returns>
    public static Coroutine Move(Transform transform, Vector3 targetPosition, float duration, bool isLocal = false)
    {
        IEnumerator Move()
        {
            var startTime = Time.time;
            var condition = true;
            
            var tempStartPosition = isLocal ? transform.localPosition : transform.position;

            while (condition)
            {
                var newDuration = (Time.time - startTime) / duration;
                var newPosition = SmoothVectors(tempStartPosition, targetPosition, newDuration);

                if (isLocal)
                {
                    transform.localPosition = newPosition;
                    condition = transform.localPosition != targetPosition;
                }
                else
                {
                    transform.position = newPosition;
                    condition = transform.position != targetPosition;
                }
                
                yield return null;
            }
        }
        
        return _instance.StartCoroutine(Move());
    }

    /// <summary>
    /// Smoothly interpolates start and end vector during duration time 
    /// </summary>
    /// <param name="start"> Start vector </param>
    /// <param name="end"> End vector </param>
    /// <param name="duration"> Time </param>
    /// <returns> Interpolated vector </returns>
    private static Vector3 SmoothVectors(Vector3 start, Vector3 end, float duration)
    {
        return new Vector3(
            Mathf.SmoothStep(start.x, end.x, duration),
            Mathf.SmoothStep(start.y, end.y, duration),
            Mathf.SmoothStep(start.z, end.z, duration));
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