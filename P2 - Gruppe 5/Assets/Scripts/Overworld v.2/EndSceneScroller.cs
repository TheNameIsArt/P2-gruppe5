using UnityEngine;

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndSceneScroller : MonoBehaviour
{
    public Rigidbody2D rb;
    public float scrollSpeed = 1f;
    public Image fadeOverlay;
    public float fadeDuration = 2f;
    public string sceneToLoad = "NextScene"; // Replace with your target scene name

    private bool hasTriggered = false;

    void Start()
    {
        if (rb == null)
            rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (!hasTriggered)
        {
            rb.linearVelocity = new Vector2(0, scrollSpeed);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!hasTriggered && other.CompareTag("scrollStop"))
        {
            hasTriggered = true;
            rb.linearVelocity = Vector2.zero;
            StartCoroutine(FadeAndLoadScene());
        }
    }

    private IEnumerator FadeAndLoadScene()
    {
        float timer = 0f;
        Color color = fadeOverlay.color;

        while (timer < fadeDuration)
        {
            timer += Time.deltaTime;
            float alpha = Mathf.Clamp01(timer / fadeDuration);
            fadeOverlay.color = new Color(color.r, color.g, color.b, alpha);
            yield return null;
        }

        SceneManager.LoadScene(sceneToLoad);
    }
}

