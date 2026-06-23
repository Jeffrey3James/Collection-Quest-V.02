using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuControls : MonoBehaviour
{
  [SerializeField] private Button startGameButton;
  [SerializeField] private Button closeGameButton;


  private void Start () {
    WireCloseButton();
    WireStartButton();
  }
  public void WireStartButton () {
    startGameButton.onClick.AddListener(() =>
    {
      Debug.Log("Start Button Wired!");
      SceneManager.LoadScene("Anni Bell Collection");
      
    });
  }

  public void WireCloseButton () {
    closeGameButton.onClick.AddListener(() =>
    {
      Debug.Log("Close Game Button Wired");
      Application.Quit();
    });
  }

  public void UnwireStartButton () {
    startGameButton.onClick.RemoveAllListeners();
  }

  public void UnWireCloseButton () {
    closeGameButton.onClick.RemoveAllListeners();
  }
}
