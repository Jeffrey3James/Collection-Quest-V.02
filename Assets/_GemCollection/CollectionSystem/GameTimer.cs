using UnityEngine;
using UnityUtils;

[System.Serializable]
public class GameTimer
{

  // Use Inheritance for the Level
    public Timer countdownTimer {  get; set; }
    public int timeConverted;

    [SerializeField] private Broadcaster timeBroadcaster;

    private const float MAX_TIME = 300f;

    public void CreateTimerAndStartTimer()
    {
        countdownTimer = new CountdownTimer(MAX_TIME);
        countdownTimer.StartTimer();
    }

    public void StopAndRecordTimer(int record) {
    record = Mathf.RoundToInt(MAX_TIME * countdownTimer.progress);
    Debug.Log(record);
    countdownTimer.StopTimer();
  }

    public int Configure(int totalTime)
    { 
       int elspsed =Mathf.RoundToInt(MAX_TIME * countdownTimer.progress);
        timeConverted = (int)MAX_TIME - elspsed;
        timeBroadcaster.Broadcast(timeConverted);
        return timeConverted;
    }

    private string GetTimeConverted()
    {
    Debug.Log(TimeUtils.ConvertIntInto_MIN_SEC_Format(timeConverted));
       return TimeUtils.ConvertIntInto_MIN_SEC_Format(timeConverted);
    }

  public void WhatIsYourName() {
    timeBroadcaster.Broadcast(Configure(timeConverted));
  }
}