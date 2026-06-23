using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LeaderboardManager : MonoBehaviour {
  private const int MAX_SCORES = 10;
  private LevelScoreTable table = new();

  private void Start () {
    Level.instance.onLevelStop += SubmitScore;
  }

  public void SubmitScore () {
    int currentLevel =
        SceneManager.GetActiveScene().buildIndex;

    LevelScoreData score = new () {
      levelName = SceneManager
      .GetActiveScene()
      .name,
      levelID = currentLevel,

      scoreThisRun = Level.instance.SaveTime()
    };

    table.levelScoreEntries.Add(score);

    // Only get scores for THIS level
    List<LevelScoreData> scoresForLevel =
        table.levelScoreEntries
        .Where(x => x.levelID == currentLevel)
        .OrderBy(x => x.scoreThisRun)
        .Take(MAX_SCORES)
        .ToList();
    // Remove old scores for this level
    table.levelScoreEntries.RemoveAll(
        x => x.levelID == currentLevel
    );

    // Add back only the top 10
    table.levelScoreEntries.AddRange(scoresForLevel);

    Save();

    Debug.Log( "Saved");
  }
  public void Save () {
    string json =
        JsonUtility.ToJson(table, true);

    File.WriteAllText(
        Path.Combine(
            Application.persistentDataPath,
            "leaderboard.json"),
        json
    );
  }

  public void Load () {
    string path =
        Path.Combine(
            Application.persistentDataPath,
            "leaderboard.json");

    if(!File.Exists(path))
      return;

    string json =
        File.ReadAllText(path);

    table =
        JsonUtility.FromJson<LevelScoreTable>(json);
  }
}