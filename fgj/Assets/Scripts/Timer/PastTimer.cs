public class PastTimer : BaseTimer
{
    bool isReleased = false;

    override public void OnTimerDone() {
        base.OnTimerDone();
        GameManager.Instance.TimeTravel();
    }

    public override void ResetTimer()
    {
        base.ResetTimer();
        isReleased = false;
    }

    override public void UpdateBehaviour()
    {
        base.UpdateBehaviour();
        if (currentTime > 1 && !isReleased) {
            GameManager.Instance.ReleaseInputLock();
            isReleased = true;
        }
    }
}
