using System.Collections;
using UnityEngine;
using System;

public class Animations : MonoBehaviour
{
    public IEnumerator Move(Transform transform, Vector3 targetPosition, float duration, bool isLocal = false)
    {
        IEnumerator Animation()
        {
            var lerpTime = 0f;
            var condition = (isLocal ? transform.localPosition : transform.position) != targetPosition;
            var startPosition = isLocal ? transform.localPosition : transform.position;
                
            while (condition)
            {
                var newPosition = Vector3.Lerp(startPosition, targetPosition, lerpTime / duration);
                    
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
                    
                lerpTime += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        return Animation();
    }

    public IEnumerator Fade(SpriteRenderer spriteRenderer, float targetAlpha, float duration)
    {
        IEnumerator Animation()
        {
            var lerpTime = 0f;
            var startColor = spriteRenderer.color;
            var targetColor = startColor;
            targetColor.a = targetAlpha;

            while(Math.Abs(spriteRenderer.color.a - targetAlpha) > 0.0001f)
            {
                spriteRenderer.color = Color.Lerp(startColor, targetColor, lerpTime / duration);

                lerpTime += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        return Animation();
    }
        
    public IEnumerator Fade(CanvasGroup canvasGroup, float targetAlpha, float duration)
    {
        IEnumerator Animation()
        {
            var lerpTime = 0f;
            var startAlpha = canvasGroup.alpha;

            while(Math.Abs(canvasGroup.alpha - targetAlpha) > 0.0001f)
            {
                canvasGroup.alpha = Mathf.Lerp(startAlpha, targetAlpha, lerpTime / duration);

                lerpTime += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        return Animation();
    }
}