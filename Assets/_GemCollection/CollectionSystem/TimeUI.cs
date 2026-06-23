using TMPro;
using UnityEngine;
using UnityUtils;

public class TimeUI : MonoBehaviour {
  [SerializeField] private Receiver timeReceiver;
  [SerializeField] TextMeshProUGUI timeScoreText;

  private void Start () {

    timeReceiver.TuneIn();
  }

  private void OnDestroy () {

    timeReceiver.TuneOut();
  }

  public void hey ( int value ) {
    timeScoreText.text = (TimeUtils.ConvertIntInto_MIN_SEC_Format(value));
  }
}