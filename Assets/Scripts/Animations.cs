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
            var cachedStartPosition = isLocal ? transform.localPosition : transform.position;
                
            while (condition)
            {
                var newPosition = Vector3.Lerp(cachedStartPosition, targetPosition, lerpTime / duration);
                    
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
            var cachedStartColor = spriteRenderer.color;
            var targetColor = spriteRenderer.color;
            targetColor.a = targetAlpha;

            while(Math.Abs(spriteRenderer.color.a - targetAlpha) > 0.0001f)
            {
                spriteRenderer.color = Color.Lerp(cachedStartColor, targetColor, lerpTime / duration);

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
            var cachedStartAlpha = canvasGroup.alpha;

            while(Math.Abs(canvasGroup.alpha - targetAlpha) > 0.0001f)
            {
                canvasGroup.alpha = Mathf.Lerp(cachedStartAlpha, targetAlpha, lerpTime / duration);

                lerpTime += Time.unscaledDeltaTime;
                yield return null;
            }
        }

        return Animation();
    }
}