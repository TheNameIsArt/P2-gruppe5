using UnityEngine;
using UnityEngine.InputSystem;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName; // The name of the scene to load

    // Method to load the scene with the specified name
    public void LoadScene()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }

    // Method to change the scene name
    public void SetSceneNameAndLoad(string newSceneName)
    {
        sceneName = newSceneName;
        if (Cursor.visible == false)
            Cursor.visible = true; // Show the cursor if it's not visible
        LoadScene();
    }
}