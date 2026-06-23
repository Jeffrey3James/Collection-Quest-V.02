using System.IO;
using UnityEngine;

#region Static class to run Save Operations

public static class SaveLoadSystem {

  #region JSON
  private const string FILE_NAME = "saveGame.json";
  private static readonly string basePath = Application.persistentDataPath;

public static string GetPath(int slotId) { return System.IO.Path.Combine(basePath, $"save_slot_{slotId}.json");
  }

public static void SaveToJson ( int slotId, GameData data ) {
  string json = JsonUtility.ToJson(data);
  File.WriteAllText(GetPath(slotId), json);
  Debug.Log($"Saved {json} to {slotId}");
}

public static GameData LoadFromJson (int slotId) {
    string path = GetPath(slotId);
  if(!File.Exists(path)) {
    Debug.LogWarning("No save file found");
    return null;
  }
  string json = File.ReadAllText(path);
    return JsonUtility.FromJson<GameData>(json);
}

public static void DeleteSave (int slotId){
    string path = GetPath(slotId);
    if(File.Exists(path)) {
    File.Delete(path);
    Debug.Log("Save Deleted");
  }
}

  public static string[] GetAllSaveFiles () {
    return Directory.GetFiles(
        basePath,
        "save_slot_*.json"
    );
  }
  #endregion


  #region Player Prefs
  private const string SAVE_KEY = "GameData";
public static void SaveToPlayerPrefs ( GameData data ) {
  string json = JsonUtility.ToJson(data);

  PlayerPrefs.SetString(SAVE_KEY, json);
  PlayerPrefs.Save();
}

public static GameData LoadFromPlayerPrefs () {
  if(!PlayerPrefs.HasKey(SAVE_KEY)) {
    return new GameData();
  }

  string json = PlayerPrefs.GetString(SAVE_KEY);
  return JsonUtility.FromJson<GameData>(json);
  }
}
#endregion

#endregion


