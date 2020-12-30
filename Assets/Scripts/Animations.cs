using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using System;
using TMPro;

public class Animations : MonoBehaviour
{
    private static Animations _instance;

    private void Awake() { _instance = this; }
    
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
    
    private static Color SmoothColorAlpha(Color start, float end, float duration)
    {
        return new Color(start.r, start.g, start.b, Mathf.SmoothStep(start.a, end, duration));
    }
}