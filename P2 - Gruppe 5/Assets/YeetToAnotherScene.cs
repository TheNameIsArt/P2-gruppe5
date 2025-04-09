using UnityEngine;
using UnityEngine.SceneManagement; // Required to change scenes

public class SceneChanger : MonoBehaviour
{
    public string sceneName; // Name of the scene to load

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Ensure only the player triggers it
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}