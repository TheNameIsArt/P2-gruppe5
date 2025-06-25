using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class EndSceneScroller : MonoBehaviour
{
    public Rigidbody2D rb;
    public float scrollSpeed = 1f;

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
            StartCoroutine(QuitAfterDelay(3f));
        }
    }

    private IEnumerator QuitAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        Application.Quit(); // This will quit the application in a build, but not in the editor
        Debug.Log("Application quit after delay.");
    }
}

