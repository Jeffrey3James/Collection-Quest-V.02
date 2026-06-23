using UnityEngine;
using System.Collections.Generic;
using UnityUtils;
using System;
using UnityEngine.UIElements;

public class Player : MonoBehaviour, ISaveable<PlayerData> {
  [SerializeField] Receiver receiver;
  private PlayerController playerController;
  private List<Timer> skillTimers = new List<Timer>();
  private Animator animator;
  public PlayerData playerData;
  public PlayerData Data => playerData;

  [SerializeField] int currentScore;

  private void Awake () {
    playerController = GetComponent<PlayerController>();

  }

  public PlayerController GetPlayerController () {
    return playerController;
  }

  private void Start () {
    animator = FindAnyObjectByType<PlayerController>().GetComponent<Animator>();

    receiver.TuneIn();
  }

  public void Increment () {
    currentScore++;
  }


  #region SaveSystem
  public PlayerData Create () {
    return new PlayerData {
      playerName = playerData.playerName,
      playerId = playerData.playerId,
    };
  }

  public void Load ( PlayerData data ) {
    playerData.playerName = data.playerName;
    playerData.playerId = data.playerId;
  }
  #endregion
}