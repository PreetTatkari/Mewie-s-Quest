using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorControl : MonoBehaviour
{
    public string sceneWithDisabledCursor;  // The name of the scene where the cursor should be disabled

    void Start()
    {
        // Check if the current scene is the one where the cursor should be disabled
        if (SceneManager.GetActiveScene().name == sceneWithDisabledCursor)
        {
            DisableCursor();
        }
    }

    void DisableCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;  // Lock the cursor to the center of the screen
        Cursor.visible = false;  // Hide the cursor
    }
}
