using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VictoryPanel : MonoBehaviour
{
    public Button ExitButton;
    public Button ReplayButton;

    private void Start()
    {
        ExitButton.onClick.AddListener(ReplayPanel.Quit);
        ReplayButton.onClick.AddListener(ReplayPanel.Replay);
    }

}
