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
        StartCoroutine(FadeFromBlack());
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
}
