using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour
{
    public Text timerText;
    // Start is called before the first frame update
    void Start()
    {
        ClearText();
    }

    void ClearText()
    {
        timerText.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.cooldownTimer == null) return;

        float remainingTime = GameManager.Instance.cooldownTimer.GetRemainingTime();
        if (remainingTime <= 0)
        {
            ClearText();
        }
        else
        {

            timerText.text = GameManager.Instance.cooldownTimer.GetRemainingTime().ToString();
        }
    }
}
