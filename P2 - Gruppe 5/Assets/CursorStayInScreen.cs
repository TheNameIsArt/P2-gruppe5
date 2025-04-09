using UnityEngine;

public class CursorStayInScreen : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Confine the cursor to the game window
        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true; // Ensure the cursor is visible
    }

    // Update is called once per frame
    void Update()
    {
        // No need to manually clamp the cursor position; CursorLockMode.Confined handles it
    }
}
