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

    public float animationTime = 1.0f;

    private bool animationIsOver = true;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        //animator.StopPlayback();
        //animator.enabled = false;
        sprite = GetComponent<SpriteRenderer>();
        transform.localScale = originalScale;
        sprite.enabled = false;
        Reset();
    }

    void Reset()
    {
        //Color newColor = new Color(1, 1, 1, 0.0f);
        //Color newColor = new Color(1, 1, 1, 0.0f);
        //sprite.material.color = newColor;
        transform.localScale = originalScale;
        //animator.enabled = false;

    }

    void StartAnimation()
    {
        //animator.enabled = true;
        animator.Play("hourglass_animation", -1, 0.0f);
        //animationIsOver = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("TimeTravel") && animationIsOver)
        {
            sprite.enabled = true;
            Reset();
            StartAnimation();
            StartCoroutine(FadeTo(0.8f, animationTime));
            StartCoroutine(Grow(animationTime));

        }
        //Debug.Log("Animator is playing: " + AnimatorIsPlaying());
        if (!animationIsOver && !AnimatorIsPlaying())
        {
            Debug.Log("animation over");
            animationIsOver = true;
            //StartCoroutine(FadeTo(0.0f, 1.0f));
            Reset();
            sprite.enabled = false;
        }
    }
    bool AnimatorIsPlaying()
    {
        return animator.GetCurrentAnimatorStateInfo(0).length >
               animator.GetCurrentAnimatorStateInfo(0).normalizedTime;
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
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / aTime)
        {
            Vector3 newScale = Vector3.Lerp(originalScale, targetScale, growSpeed.Evaluate(t));
            transform.localScale = newScale;

            yield return null;
        }
    }

}
