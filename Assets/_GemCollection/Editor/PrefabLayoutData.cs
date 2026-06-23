using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Tools/Prefab Layout Data")]
public class PrefabLayoutData : ScriptableObject {
  public List<PlacedPrefabData> placedPrefabs = new();
}
