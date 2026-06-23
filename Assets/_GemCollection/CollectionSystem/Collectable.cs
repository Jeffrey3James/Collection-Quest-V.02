using UnityEngine;
/*using StroTheGoat;*/

public class Collectable : MonoBehaviour
{
    [SerializeField] private Broadcaster scoreBroadcaster;

  private void OnEnable () {
    Level.instance.SetCurrentBroadcaster(
      scoreBroadcaster
      );
  }

  private void OnTriggerEnter(Collider other)
    {
    scoreBroadcaster.Broadcast(1);
    gameObject.SetActive(false);

    Debug.Log($"{Level.instance.CurrentScore} / {Level.instance.Setting.spawnPoints.Length}");
    if(Level.instance.CurrentScore == Level.instance.Setting.spawnPoints.Length) {
      Level.instance.LevelStop();
    }
    }
}


