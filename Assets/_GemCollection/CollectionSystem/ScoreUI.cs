using System.Net.Security;
using System.Runtime.CompilerServices;
using System.Xml.Serialization;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour {
  [SerializeField] private Receiver receiver;
  [SerializeField] TextMeshProUGUI scoreText;
  [SerializeField] int winningScore;

  private const int STARTING_SCORE = 0;

  private void Start () {
    winningScore = STARTING_SCORE;

    winningScore = Level.instance.Setting.spawnPoints.Length;
    receiver.TuneIn();
    UpdateScoreUI();
  }

  public void Increment () {
    UpdateScoreUI();
  }

  private void OnDestroy () {
    receiver.TuneOut();
  }

  public void UpdateScoreUI () {
    int currentScore = Level.instance.CurrentScore;

    scoreText.text = ($"{currentScore} / {winningScore.ToString()}");
  }

}
