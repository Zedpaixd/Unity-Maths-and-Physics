using UnityEditor;
using UnityEngine.SceneManagement;
using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public static void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
        Application.Quit(0);
#endif
    }

    public static void Replay()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
