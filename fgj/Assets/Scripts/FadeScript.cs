using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeScript : MonoBehaviour
{
    private bool couroutineIsDone = true;
    SpriteRenderer spriteRenderer;
    CanvasGroup canvasGroup;
    public AnimationCurve fadeCurve;

    public bool fadingOut = false;

    public bool fadingIn = false;

    public float fadeSpeed = 10f;


    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null) {
            canvasGroup = GetComponent<CanvasGroup>();
        }
    }

    void UpdateCanvasGroup()
    {
        if (fadingIn)
        {
            if (canvasGroup.alpha < 1)
            {
                canvasGroup.alpha = canvasGroup.alpha + fadeSpeed * Time.deltaTime;
            }
            else
            {
                fadingIn = false;
                fadingOut = false;
            }

        }

        if (fadingOut)
        {
            if (canvasGroup.alpha > 0)
            {
                canvasGroup.alpha = canvasGroup.alpha- fadeSpeed * Time.deltaTime;
            }
            else
            {
                fadingIn = false;
                fadingOut = false;
            }
        }
    }

    void UpdateSpriteRenderer()
    {
        if (fadingIn)
        {
            if (spriteRenderer.material.color.a < 1)
            {
                Color newColor = new Color(1, 1, 1, spriteRenderer.material.color.a + fadeSpeed * Time.deltaTime);
                spriteRenderer.material.color = newColor;
            }
            else
            {
                fadingIn = false;
                fadingOut = false;
            }

        }

        if (fadingOut)
        {
            if (spriteRenderer.material.color.a > 0)
            {
                Color newColor = new Color(1, 1, 1, spriteRenderer.material.color.a - fadeSpeed * Time.deltaTime);
                spriteRenderer.material.color = newColor;
            }
            else
            {
                fadingIn = false;
                fadingOut = false;
            }
        }
    }

    void Update()
    {
        if (spriteRenderer != null) {
            UpdateSpriteRenderer();
        }

        if (canvasGroup != null) {
            UpdateCanvasGroup();
        }
    }

    public void Fade(float time, bool willBeActive)
    {
        var targetAlpha = willBeActive ? 1.0f : 0f;
        StartCoroutine(FadeTo(targetAlpha, 0.5f, willBeActive));
    }

    IEnumerator FadeTo(float aValue, float aTime, bool willBeActive)
    {
        float currentAlpha = willBeActive ? 0.0f : 1.0f;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(currentAlpha, aValue, fadeCurve.Evaluate(t)));
            spriteRenderer.material.color = newColor;
            yield return null;
        }
        if (!willBeActive)
        {
            Debug.Log("disabling");
            gameObject.SetActive(false);
        }
    }
}
