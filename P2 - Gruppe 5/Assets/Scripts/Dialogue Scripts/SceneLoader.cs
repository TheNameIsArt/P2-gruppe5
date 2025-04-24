using UnityEngine;
using UnityEngine.InputSystem;

public class SceneLoader : MonoBehaviour
{

    // Method to load the scene with the specified name
    public void LoadScene(string sceneName)
    {
        if (Cursor.visible == false)
            Cursor.visible = true; // Show the cursor if it's not visible
        SceneFader.Instance.FadeToScene(sceneName);
    }
}