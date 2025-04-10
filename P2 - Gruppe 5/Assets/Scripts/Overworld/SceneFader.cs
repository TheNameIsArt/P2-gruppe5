using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance { get; private set; }

    public Image fadeImage;
    public float fadeDuration = 1f;
    public float fastFadeDuration = 0.7f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (fadeImage != null)
            fadeImage.enabled = false; // Make sure it's off at start
    }

    private void Start()
    {
        Vector3 savedPosition = SceneFader.Instance.LoadPlayerPosition(SceneManager.GetActiveScene().name);
        if (savedPosition != Vector3.zero)
        {
            Debug.Log($"Restoring player position to: {savedPosition}");
            transform.position = savedPosition;
        }
        else
        {
            Debug.Log("No saved position found. Using default position.");
        }
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    public void FastFadeToScene(string sceneName)
    {
        StartCoroutine(FastFadeAndLoadScene(sceneName));
    }

    IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Save the player's position
        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            SavePlayerPosition(player.transform.position, SceneManager.GetActiveScene().name);
        }

        yield return StartCoroutine(FadeToBlack());
        SceneManager.LoadScene(sceneName);
        yield return new WaitForEndOfFrame();
        StartCoroutine(FadeFromBlack());
    }
    IEnumerator FastFadeAndLoadScene(string sceneName)
    {
        yield return StartCoroutine(FastFadeToBlack());
        SceneManager.LoadScene(sceneName);
        yield return new WaitForEndOfFrame();
        StartCoroutine(FastFadeFromBlack());
    }



    IEnumerator FadeToBlack()
    {
        fadeImage.enabled = true;
        float t = 0f;
        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1);
    }

    IEnumerator FadeFromBlack()
    {
        fadeImage.enabled = true;
        float t = fadeDuration;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.enabled = false;
    }
    IEnumerator FastFadeToBlack()
    {
        fadeImage.enabled = true;
        float t = 0f;
        while (t < fastFadeDuration)
        {
            t += Time.deltaTime;
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 1);
    }

    IEnumerator FastFadeFromBlack()
    {
        fadeImage.enabled = true;
        float t = fastFadeDuration;
        while (t > 0f)
        {
            t -= Time.deltaTime;
            float alpha = t / fadeDuration;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.enabled = false;
    }

    public void SavePlayerPosition(Vector3 position, string sceneName)
    {
        Debug.Log($"Saving position for scene {sceneName}: {position}");
        PlayerPrefs.SetFloat($"{sceneName}_PlayerPosX", position.x);
        PlayerPrefs.SetFloat($"{sceneName}_PlayerPosY", position.y);
        PlayerPrefs.SetFloat($"{sceneName}_PlayerPosZ", position.z);
        PlayerPrefs.Save();
    }

    public Vector3 LoadPlayerPosition(string sceneName)
    {
        if (PlayerPrefs.HasKey($"{sceneName}_PlayerPosX"))
        {
            float x = PlayerPrefs.GetFloat($"{sceneName}_PlayerPosX");
            float y = PlayerPrefs.GetFloat($"{sceneName}_PlayerPosY");
            float z = PlayerPrefs.GetFloat($"{sceneName}_PlayerPosZ");
            Vector3 loadedPosition = new Vector3(x, y, z);
            Debug.Log($"Loaded position for scene {sceneName}: {loadedPosition}");
            return loadedPosition;
        }

        Debug.Log($"No saved position found for scene {sceneName}. Returning default position.");
        return Vector3.zero;
    }
}
