using UnityEngine;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public string targetSceneName;  // The name of the scene where the cursor should be enabled

    void Start()
    {
        // Check if the current scene is the target scene
        if (SceneManager.GetActiveScene().name == targetSceneName)
        {
            EnableCursor();
        }
    }

    void EnableCursor()
    {
        Cursor.lockState = CursorLockMode.None;  // Unlock the cursor
        Cursor.visible = true;  // Make the cursor visible
    }
}