using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text timerText;

    public HealthBarComponent barComponent;
    public Image fillImage;

    public Color originalColor;

    // Start is called before the first frame update
    void Start()
    {
        barComponent.Initialize(GameManager.Instance.COOLDOWN_MAX_SECONDS, false);
        originalColor = fillImage.color;
    }

    public void OnEnterPast()
    {
        Debug.Log("OnEnterPast in TimerUi");
        barComponent.SetValue(GameManager.Instance.PAST_MAX_SECONDS);
        fillImage.color = new Color(255, 255, 20.0f);
        barComponent.slider.maxValue = GameManager.Instance.PAST_MAX_SECONDS;
    }

    public void OnEnterFuture()
    {
        Debug.Log("OnEnterFuture in TimerUi");
        barComponent.SetValue(0);
        fillImage.color = originalColor;
        barComponent.slider.maxValue = GameManager.Instance.COOLDOWN_MAX_SECONDS;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.inFuture)
        {
            if (GameManager.Instance.cooldownTimer == null) return;

            float remainingTime = GameManager.Instance.cooldownTimer.GetRemainingTime();
            barComponent.SetValue(GameManager.Instance.COOLDOWN_MAX_SECONDS - remainingTime);
        }
        else
        {
            if (GameManager.Instance.pastTimer == null) return;

            float remainingTime = GameManager.Instance.pastTimer.GetRemainingTime();
            barComponent.SetValue(remainingTime);
        }

    }
}
