using System;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class Fade : MonoBehaviour
{
    [SerializeField] private Image screen;
    [SerializeField] private float fadeDuration = 1f;
    public void FadeIn(Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(0f, 1f, onComplete));
    }

    public void FadeOut(Action onComplete = null)
    {
        StartCoroutine(FadeCoroutine(1f, 0f, onComplete));
    }

    private IEnumerator FadeCoroutine(float startAlpha, float endAlpha, Action onComplete = null)
    {
        float elapsedTime = 0f;
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsedTime / fadeDuration);
            screen.color = new Color(screen.color.r, screen.color.g, screen.color.b, alpha);
            yield return null;
        }
        screen.color = new Color(screen.color.r, screen.color.g, screen.color.b, endAlpha);
        onComplete?.Invoke();
    }
}