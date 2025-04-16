using UnityEngine;

public class AudioSingleton : MonoBehaviour
{
    public static AudioSingleton Instance { get; private set; }
    private AudioSource audioSource;
    private bool isFadingOut = false;
    private float fadeOutSpeed = 1f; // Speed at which the volume decreases

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }

        // Get the AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogWarning("No AudioSource found on Singleton. Add one to use volume fading.");
        }
    }

    private void Update()
    {
        // Handle volume fading
        if (isFadingOut && audioSource != null)
        {
            audioSource.volume -= fadeOutSpeed * Time.deltaTime;
            if (audioSource.volume <= 0f)
            {
                audioSource.volume = 0f;
                Destroy(gameObject);
            }
        }
    }

    // Method to start fading out and destroy the singleton
    public void FadeOutAndDestroy(float speed)
    {
        if (audioSource == null)
        {
            Debug.LogWarning("Cannot fade out. No AudioSource attached.");
            return;
        }

        fadeOutSpeed = speed;
        isFadingOut = true;
    }
}
