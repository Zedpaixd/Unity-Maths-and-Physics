using UnityEngine;

public class CursorBehaviour : MonoBehaviour
{

    [SerializeField] private KeyCode visibilityKey;

    void Start()
    {
        //hide cursor
        Cursor.visible = false;

        //lock cursor to middle of screen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        ToggleCursorVisibility();
    }

    void ToggleCursorVisibility() 
    {
        if (Input.GetKey(visibilityKey))
        {
            //show cursor
            Cursor.visible = true;

            //fre cursor
            Cursor.lockState = CursorLockMode.None;
        }
        else if (Input.GetKeyUp(visibilityKey))
        {
            //hide cursor
            Cursor.visible = false;

            //lock cursor to middle of screen
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
