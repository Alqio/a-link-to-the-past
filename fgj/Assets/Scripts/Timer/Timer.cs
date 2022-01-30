using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseTimer : MonoBehaviour
{

    public float duration;
    public float currentTime = 0;
    public bool isDone = false;

    public bool isStarted = false;

    public void SetDuration(float duration) {
        this.duration = duration;
    }

    public void StartTimer() {
        isStarted = true;
    }

    public float GetRemainingTime() {
        return duration - currentTime;
    }

    public virtual void ResetTimer()
    {
        currentTime = 0;
        isDone = false;
        isStarted = true;
    }

    public virtual void UpdateBehaviour() {
        if (isStarted && !isDone) {
            currentTime += Time.deltaTime;
            if (currentTime >= duration) {
                OnTimerDone();
                currentTime = duration;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        UpdateBehaviour();
    }

    public virtual void OnTimerDone() {
        isDone = true;
    }
}