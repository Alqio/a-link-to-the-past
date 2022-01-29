using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CooldownTimer : BaseTimer
{
    override public void OnTimerDone() {
        base.OnTimerDone();
        GameManager.Instance.EndCooldown();
    }
}
