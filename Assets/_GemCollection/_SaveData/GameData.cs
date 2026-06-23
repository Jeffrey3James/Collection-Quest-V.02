using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;



public interface ISaveable<T> {
  T Create ();
  void Load ( T data );
}

[Serializable]
public class GameData {

  public PlayerData playerData;
  public LevelScoreData levelScoreData;
}

[Serializable]
public class PlayerData {
  public string playerName;
  public int playerId;
  public int level;
  public int time;
}

[Serializable]
public class LevelScoreData {
  public string saveDate;
  public string levelName;
  public int levelID;
  public int scoreThisRun;
}

[Serializable]
public class LevelScoreTable {
  public List<LevelScoreData> levelScoreEntries = new();
}
