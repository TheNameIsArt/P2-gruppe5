using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class SceneFader : MonoBehaviour
{
    public static SceneFader Instance { get; private set; }

    public Image fadeImage;
    public float fadeDuration = 2f;
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
        StartCoroutine(FadeFromBlack());
    }

    public void FadeToScene(string sceneName)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true); // Activate the GameObject if it's inactive
        }

        StartCoroutine(FadeAndLoadScene(sceneName));
    }

    public void FastFadeToScene(string sceneName)
    {
        StartCoroutine(FastFadeAndLoadScene(sceneName));
    }

    IEnumerator FadeAndLoadScene(string sceneName)
    {
        // Call FadeOutAndDestroy on the Singleton instance if it exists
        if (AudioSingleton.Instance != null)
        {
            AudioSingleton.Instance.FadeOutAndDestroy(1f); // Adjust the fade-out speed as needed
            // Wait for the singleton to finish fading out
            yield return new WaitUntil(() => Singleton.Instance == null);
        }

        // Proceed with fading to black and loading the new scene
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
    public void FadeIn()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true); // Ensure the GameObject is active
        }

        StartCoroutine(FadeToBlack());
    }

    public void FadeOut()
    {
        if (!gameObject.activeSelf)
        {
            gameObject.SetActive(true); // Ensure the GameObject is active
        }

        StartCoroutine(FadeFromBlack());
    }
}
