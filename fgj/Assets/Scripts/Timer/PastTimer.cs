using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PastTimer : BaseTimer
{
    override public void OnTimerDone() {
        base.OnTimerDone();
        GameManager.Instance.TimeTravel();
    }
}
