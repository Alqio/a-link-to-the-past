using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourglass : MonoBehaviour
{

    private Animator animator;
    private float direction = -1.0f;

    public Vector3 originalScale = new Vector3(2, 2, 2);
    public Vector3 targetScale = new Vector3(100, 100, 100);
    SpriteRenderer sprite;

    public AnimationCurve growSpeed;

    public float animationTime = 2.0f;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator.StopPlayback();
        animator.enabled = false;
        sprite = GetComponent<SpriteRenderer>();
        transform.localScale = originalScale;

        Reset();
    }

    void Reset()
    {
        Color newColor = new Color(1, 1, 1, 0.0f);
        sprite.material.color = newColor;
        transform.localScale = originalScale;
        animator.enabled = false;
    }
    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
    }
    void StartAnimation()
    {
        animator.enabled = true;
        animator.Play("reverse", -1, 0.0f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TimeTravel"))
        {
            Reset();
            StartAnimation();
            StartCoroutine(FadeTo(0.8f, animationTime));
            StartCoroutine(Grow(animationTime));
        }
    }

    public IEnumerator FadeTo(float aValue, float aTime)
    {
        float alpha = sprite.material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            var newOpacity = Mathf.Lerp(alpha, aValue, t);
            Color newColor = new Color(1, 1, 1, newOpacity);
            sprite.material.color = newColor;
            yield return null;
        }
    }

    public IEnumerator Grow(float aTime)
    {
        var scale = originalScale.x;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Vector3 newScale = Vector3.Lerp(originalScale, targetScale, growSpeed.Evaluate(t));
            transform.localScale = newScale;

            yield return null;
        }
    }

}
