using UnityEngine;

[ExecuteAlways]
[System.Serializable]
public class LevelSetting {
  public string levelId;
  public string levelName;
  public GameObject collectablePrefab;
  public Transform[] spawnPoints;

  public LevelSetting () { }

  public class Builder {
    private string levelId;
    private string levelName;
    private int maxCollectibles;
    private GameObject prefab;
    private Transform[] spawnPoints;

    public string LevelId => levelId;
    public string LevelName => levelName;
    public int MaxCollectibles => maxCollectibles;
    public Transform[] SpawnPoints => spawnPoints;

    public Builder WithLevelId ( string levelId ) {
      this.levelId = levelId;
      return this;
    }

    public Builder WithLevelName ( string levelName ) {
      this.levelName = levelName;
      return this;
    }

    public Builder WithMaxCollectibles ( int maxCollectibles ) {
      this.maxCollectibles = maxCollectibles;
      return this;
    }

    public Builder WithPrefab ( GameObject prefab ) {
      this.prefab = prefab;
      return this;
    }

    public Builder WithSpawnPoints ( Transform[] spawnPoints ) {
      this.spawnPoints = spawnPoints;
      return this;
    }
    public Builder WithFactory ( ICollectableFactory factory ) {
      prefab = factory.Supply();
      return this;
    }
    public LevelSetting Build () {
      return new LevelSetting {
        levelId = this.levelId,
        levelName = this.levelName,
        collectablePrefab = this.prefab,
        spawnPoints = this.spawnPoints,
      };
    }
  }
}
