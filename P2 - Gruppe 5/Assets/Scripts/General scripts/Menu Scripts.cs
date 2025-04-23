using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

public class MenuScripts : MonoBehaviour
{
    public static MenuScripts Instance { get; private set; } // Singleton instance

    [SerializeField] private GameObject gameOverPanel; // Assign the GameObject in the Inspector
    [SerializeField] private GameObject tryAgainButton; // Assign the "Try Again" button in the Inspector

    private void Awake()
    {
        // Ensure only one instance of the singleton exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: Keep this object across scenes
    }

    public void GameOver()
    {
        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true); // Activate the GameObject
        }

        StartCoroutine(GraduallyReduceTime());
    }

    private System.Collections.IEnumerator GraduallyReduceTime()
    {
        float currentTime = 1f;

        while (currentTime > 0f)
        {
            currentTime -= Time.deltaTime; // Decrease time gradually
            Time.timeScale = currentTime; // Adjust the game's time scale
            yield return null; // Wait for the next frame
        }

        Time.timeScale = 0f; // Ensure time is completely stopped

        // Set the current button in the EventSystem to the "Try Again" button
        if (tryAgainButton != null)
        {
            EventSystem.current.SetSelectedGameObject(tryAgainButton);
        }
    }
    public void TryAgain()
    {
        StopAllCoroutines(); // Stop any running coroutines
        Time.timeScale = 1f; // Reset time scale to normal
        gameOverPanel.SetActive(false); // Deactivate the GameOver panel
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload the current scene
        Debug.Log("Try Again button clicked! Scene reloaded.");
    }
}
