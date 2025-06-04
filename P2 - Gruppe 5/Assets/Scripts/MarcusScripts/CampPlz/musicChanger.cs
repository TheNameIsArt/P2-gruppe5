using UnityEngine;

public class musicChanger : MonoBehaviour
{
    [SerializeField] private AudioClip newClip; // The new AudioClip to play
    [SerializeField] private float fadeDuration = 1f; // Duration of the fade effect

    // Start is called before the first frame update
    void Start()
    {
        // Find the singleton GameObject named "Background music"
        GameObject backgroundMusicObject = GameObject.Find("Background music");
        if (backgroundMusicObject == null)
        {
            Debug.LogError("Background music GameObject not found!");
            return;
        }

        // Get the AudioSource component from the "Background music" GameObject
        AudioSource audioSource = backgroundMusicObject.GetComponent<AudioSource>();
        if (audioSource == null)
        {
            Debug.LogError("AudioSource component is missing on the 'Background music' GameObject!");
            return;
        }

        if (newClip == null)
        {
            Debug.LogError("New AudioClip is not assigned in the Inspector!");
            return;
        }

        // Change the music with a fade effect
        StartCoroutine(FadeMusic(audioSource));
    }

    private System.Collections.IEnumerator FadeMusic(AudioSource audioSource)
    {
        float initialVolume = audioSource.volume;

        // Fade out the current audio
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(initialVolume, 0, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = 0; // Ensure volume is completely off
        audioSource.clip = newClip; // Assign the new AudioClip
        audioSource.Play(); // Play the new clip

        // Fade in the new audio
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            audioSource.volume = Mathf.Lerp(0, initialVolume, t / fadeDuration);
            yield return null;
        }

        audioSource.volume = initialVolume; // Ensure volume is fully restored
        Debug.Log("Audio clip changed with fade effect and started playing on 'Background music'.");
    }
}
