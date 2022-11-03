using UnityEngine;
using TMPro;

public class DebugText : MonoBehaviour
{
    public  static TextMeshProUGUI text;

    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
    }

    public static void SetDebugText(string textToShow)
    {
        text.text = textToShow;
    }
}
