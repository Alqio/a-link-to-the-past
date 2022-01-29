using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarComponent : MonoBehaviour
{

    public Slider slider;

    float targetValue;
    float currentValue;
    bool isLerp;

    float moveSpeed = 200.0f;

    public void Initialize(float health, bool isLerp = true)
    {
        currentValue = health;
        SetValue(health);
        this.isLerp = isLerp;
        slider.maxValue = health;
    }

    public void SetValue(float health)
    {
        targetValue = health;

        if (!isLerp) {
            currentValue = targetValue;
            slider.value = currentValue;
        }
    }

    void Update() {

        if (!isLerp) {
            return;
        }

        if (Mathf.Abs(currentValue - targetValue) < 0.1)
        {
            slider.value = targetValue;
            return;
        }

        float direction = Mathf.Sign(currentValue - targetValue);
        currentValue = currentValue - Time.deltaTime * moveSpeed * direction;
        slider.value = currentValue;
    }
}
