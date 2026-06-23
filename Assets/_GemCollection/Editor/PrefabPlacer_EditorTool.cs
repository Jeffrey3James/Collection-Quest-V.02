using System;
using UnityEditor;
using UnityEngine;

public class PrefabPlacer_EditorTool : EditorWindow {
  private GameObject prefabToPlace;
  private PrefabLayoutData layoutData;

  private int maxCount = 10;
  private int currentCount = 0;
  private float width = 10f;
  private float length = 10f;
  private Transform origin;

  [MenuItem("Tools/Prefab Placer Tool")]
  public static void Open () {
    GetWindow<PrefabPlacer_EditorTool>("Prefab Placer");
  }

  private void OnGUI () {
    GUILayout.Label("Prefab Placement Tool", EditorStyles.boldLabel);

    prefabToPlace = (GameObject)EditorGUILayout.ObjectField("Prefab", prefabToPlace, typeof(GameObject), false);
    layoutData = (PrefabLayoutData)EditorGUILayout.ObjectField("Layout Data", layoutData, typeof(PrefabLayoutData), false);

    maxCount = EditorGUILayout.IntField("Max Count", maxCount);
    width = EditorGUILayout.FloatField("Width", width);
    length = EditorGUILayout.FloatField("Length", length);
    origin = (Transform)EditorGUILayout.ObjectField("Origin", origin, typeof(Transform), true);

    GUILayout.Space(10);

    if(GUILayout.Button("Spawn Prefab")) {
      SpawnPrefab();
    }

    if(GUILayout.Button("Save Scene Objects")) {
      SaveSceneObjects();
    }

    if(GUILayout.Button("Clear Spawned (Editor Only)")) {
      ClearSpawned();
    }
  }

  private void SpawnPrefab () {

    if(prefabToPlace == null) return;
    if(currentCount >= maxCount) return;

    float halfWidth = width * 0.5f;
    float halfLength = length * 0.5f;

    float randomX = UnityEngine.Random.Range(-halfWidth, halfWidth);
    float randomZ = UnityEngine.Random.Range(-halfLength, halfLength);

    Vector3 center = origin ? origin.position : Vector3.zero;
    Vector3 spawnPos = center + new Vector3(randomX, 0f, randomZ);

    GameObject obj = (GameObject)PrefabUtility.InstantiatePrefab(prefabToPlace);
    obj.transform.position = spawnPos;

    currentCount++;
  }

  private void SaveSceneObjects () {
    if(layoutData == null) return;

    layoutData.placedPrefabs.Clear();

    foreach(GameObject obj in GameObject.FindGameObjectsWithTag("PlacedPrefab")) {
      layoutData.placedPrefabs.Add(new PlacedPrefabData {
        prefab = prefabToPlace,
        position = obj.transform.position,
        rotation = obj.transform.rotation
      });
    }

    EditorUtility.SetDirty(layoutData);
    AssetDatabase.SaveAssets();
  }

  private void ClearSpawned () {
    foreach(GameObject obj in GameObject.FindGameObjectsWithTag("PlacedPrefab")) {
      DestroyImmediate(obj);
    }

    currentCount = 0;
  }
}

[Serializable]
public class PlacedPrefabData {
  public GameObject prefab;
  public Vector3 position;
  public Quaternion rotation;
}