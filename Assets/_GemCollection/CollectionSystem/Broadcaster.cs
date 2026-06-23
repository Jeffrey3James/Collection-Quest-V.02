using System.Collections.Generic;
using System.Diagnostics.Contracts;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = " Events / Int Event")]
public class Broadcaster : ScriptableObject
{
    readonly HashSet<Receiver> receivers = new();

    public void Broadcast(int value)
    {
        foreach (var receiver in receivers)
        {
            receiver.Raise(value);
        }
    }

    public void Subscribe(Receiver reciver) => receivers.Add(reciver);

    public void UnSubscribe(Receiver reciver) => receivers.Remove(reciver);
}