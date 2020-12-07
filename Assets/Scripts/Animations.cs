using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class Animations : MonoBehaviour
{
    private static Animations _instance;

    private void Awake() { _instance = this; }

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
            var startTime = Time.realtimeSinceStartup;
            var condition = (isLocal? transform.localPosition : transform.position) != targetPosition;
            
            var tempStartPosition = isLocal ? transform.localPosition : transform.position;

            while (condition)
            {
                var normalizedTime = (Time.realtimeSinceStartup - startTime) / duration;
                var newPosition = SmoothVectors(tempStartPosition, targetPosition, normalizedTime);

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
    /// Smoothly fades the sprite color in or out to the target alpha value over time
    /// </summary>
    /// <param name="spriteRendererToFade"> Sprite to fade </param>
    /// <param name="targetAlpha"> Target alpha </param>
    /// <param name="duration"> Animation duration in seconds </param>
    /// <returns> Animation </returns>
    public static Coroutine Fade(SpriteRenderer spriteRendererToFade, float targetAlpha, float duration)
    {
        IEnumerator Fade()
        {
            spriteRendererToFade.gameObject.SetActive(true);

            var startTime = Time.realtimeSinceStartup;

            while (Math.Abs(spriteRendererToFade.color.a - targetAlpha) > 0.0001f)
            {
                var normalizedTime = (Time.realtimeSinceStartup - startTime) / duration;
                var newColor = SmoothColorAlpha(spriteRendererToFade.color, targetAlpha, normalizedTime);

                spriteRendererToFade.color = newColor;

                yield return null;
            }
            
            spriteRendererToFade.gameObject.SetActive(targetAlpha > 0.001f); // deactivate if invisible
        }

        return _instance.StartCoroutine(Fade());
    }

    /// <summary>
    /// Smoothly fades the text color in or out to the target alpha value over time
    /// </summary>
    /// <param name="textToFade"> Text to fade </param>
    /// <param name="targetAlpha"> Target alpha </param>
    /// <param name="duration"> Animation duration in seconds </param>
    /// <returns> Animation </returns>
    public static Coroutine Fade(TextMeshProUGUI textToFade, float targetAlpha, float duration)
    {
        IEnumerator Fade()
        {
            textToFade.gameObject.SetActive(true);
            
            var startTime = Time.realtimeSinceStartup;

            while (Math.Abs(textToFade.color.a - targetAlpha) > 0.0001f)
            {
                var normalizedTime = (Time.realtimeSinceStartup - startTime) / duration;
                var newColor = SmoothColorAlpha(textToFade.color, targetAlpha, normalizedTime);

                textToFade.color = newColor;

                yield return null;
            }
            
            textToFade.gameObject.SetActive(targetAlpha > 0.001f); // deactivate if invisible
        }

        return _instance.StartCoroutine(Fade());
    }

    /// <summary>
    /// Smoothly fades the image color in or out to the target alpha value over time
    /// </summary>
    /// <param name="imageToFade"> Image to fade </param>
    /// <param name="targetAlpha"> Target alpha </param>
    /// <param name="duration"> Animation duration in seconds </param>
    /// <returns> Animation </returns>
    public static Coroutine Fade(Image imageToFade, float targetAlpha, float duration)
    {
        IEnumerator Fade()
        {
            imageToFade.gameObject.SetActive(true);
            
            var startTime = Time.realtimeSinceStartup;

            while (Math.Abs(imageToFade.color.a - targetAlpha) > 0.0001f)
            {
                var normalizedTime = (Time.realtimeSinceStartup - startTime) / duration;
                var newColor = SmoothColorAlpha(imageToFade.color, targetAlpha, normalizedTime);

                imageToFade.color = newColor;

                yield return null;
            }
            
            imageToFade.gameObject.SetActive(targetAlpha > 0.001f); // deactivate if invisible
        }

        return _instance.StartCoroutine(Fade());
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
    /// Smoothly interpolates start and end color alpha during duration time
    /// </summary>
    /// <param name="start"> Start color </param>
    /// <param name="end"> End alpha </param>
    /// <param name="duration"> Time </param>
    /// <returns> Interpolated color </returns>
    private static Color SmoothColorAlpha(Color start, float end, float duration)
    {
        return new Color(start.r, start.g, start.b, Mathf.SmoothStep(start.a, end, duration));
    }

    /// <summary>
    /// Stops the animation
    /// </summary>
    /// <param name="animation"> Animation to stop </param>
    public static void Stop(Coroutine animation)
    {
        if (animation != null)
            _instance.StopCoroutine(animation);
    }
}