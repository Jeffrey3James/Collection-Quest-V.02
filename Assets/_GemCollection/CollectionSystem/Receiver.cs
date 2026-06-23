using System;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class Receiver
{
    [SerializeField] Broadcaster broadcaster;
    [SerializeField] UnityEvent<int> unityEvent;

    public void TuneIn()
    {
        broadcaster.Subscribe(this);
    }

    public void TuneOut()
    {
        broadcaster.UnSubscribe(this);
    }

  public void SetBroadcaster ( Broadcaster broadcaster ) {this.broadcaster = broadcaster; 
    TuneIn();
  } 

  public Broadcaster GetCurrentBroadcaster() {
    return broadcaster;
  }

  public void Raise(int value)
    {
        unityEvent?.Invoke(value);
    }
}
