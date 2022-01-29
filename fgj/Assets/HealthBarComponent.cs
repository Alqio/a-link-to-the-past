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

    void FixedUpdate()
    {
        if (Mathf.Abs(currentValue - targetValue) < 2)
        {
            slider.value = targetValue;
            return;
        }
        float direction = Mathf.Sign(currentValue - targetValue);
        currentValue = currentValue - Time.fixedDeltaTime * moveSpeed * direction;
        slider.value = currentValue;
    }
}
