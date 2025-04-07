using UnityEngine;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string sceneName; // The name of the scene to load
    public void loadScene()
    {
        // Load the scene with the specified name
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
    }
}
