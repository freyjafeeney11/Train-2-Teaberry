using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MoveRoomTransition : MonoBehaviour
{
    public Image fadeImage; // assign the black panel's Image here
    public float fadeDuration = 0.5f;

    private void Awake()
    {
        // Start transparent
        SetAlpha(0f);
    }

    public void FadeToBlack(System.Action onComplete = null)
    {
        StartCoroutine(Fade(0f, 1f, onComplete));
    }

    public void FadeFromBlack(System.Action onComplete = null)
    {
        StartCoroutine(Fade(1f, 0f, onComplete));
    }

    IEnumerator Fade(float startAlpha, float endAlpha, System.Action onComplete)
    {
        float elapsed = 0f;

        while (elapsed < fadeDuration)
        {
            elapsed += Time.deltaTime;
            float alpha = Mathf.Lerp(startAlpha, endAlpha, elapsed / fadeDuration);
            SetAlpha(alpha);
            yield return null;
        }

        SetAlpha(endAlpha);
        onComplete?.Invoke();
    }

    void SetAlpha(float alpha)
    {
        Color c = fadeImage.color;
        c.a = alpha;
        fadeImage.color = c;
    }
}
