using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarComponent : MonoBehaviour
{

    public Slider slider;

    float targetValue;
    float currentValue;

    float moveSpeed = 200.0f;

    public void Initialize(float health)
    {
        currentValue = health;
        SetValue(health);
    }

    public void SetValue(float health)
    {
        targetValue = health;
    }

    void Update() {
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
