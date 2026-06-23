using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

//public interface ILevelGenerator {
//  void Generate ();
//}

public class Level : MonoBehaviour {
  public static Level instance {get; private set; }

  private int currentScore;

  [SerializeField] private Receiver receiver;
  [SerializeField] private LevelSetting setting;
  [SerializeField] private GameTimer gameTimer;
  [SerializeField] private int totalTime;



  public LevelSetting Setting => setting;
  public int TotalTime => totalTime;
  public int CurrentScore => currentScore;

  public event UnityAction onLevelStart;
  public event UnityAction onLevelStop;

  public void SetCurrentBroadcaster(Broadcaster newBroadcaster) {
    receiver.SetBroadcaster(newBroadcaster);
  }

  private void Awake () {
    instance = this;
    setting = new LevelSetting.Builder()
        .WithLevelId("ID: Test Level")
        .WithLevelName("LevelName: Test Level")
        .WithPrefab(setting.collectablePrefab)
        .WithSpawnPoints(setting.spawnPoints)
        .Build();

    onLevelStart += StartGame;
    onLevelStop += FinishGame;

    foreach(Transform spawnPoint in setting.spawnPoints) {
      GameObject collectable = Instantiate(setting.collectablePrefab, spawnPoint.position, spawnPoint.rotation);
      collectable.gameObject.SetActive(true);
    }
  }
  private void Start () { 
    onLevelStart.Invoke();
  }

  public void LevelStop () { 
    onLevelStop.Invoke();
    Debug.Log("Finished");
  }

  private void Update () {
    gameTimer.countdownTimer.Tick(Time.deltaTime);
    gameTimer.WhatIsYourName();
  }

  public void StartGame() {
    //Start Timer
    gameTimer.CreateTimerAndStartTimer();

    Debug.Log("Game Started");
  }

  public int SaveTime() {
    return gameTimer.timeConverted;
  }

  public void FinishGame() {
    gameTimer.StopAndRecordTimer(totalTime);

    SceneManager.LoadScene("Main Menu");
    Debug.Log("Game Finished");
  }

  public void Call () {
    currentScore++;
  }

  #region Editor Util
  public void Generate() {
    foreach(Transform point in setting.spawnPoints) {      
      Instantiate(
        setting.collectablePrefab);
    }
  }
  #endregion
}
