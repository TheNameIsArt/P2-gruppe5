using UnityEngine;
using UnityEngine.InputSystem;

public class SpecialPhoneBox : MonoBehaviour
{
    [SerializeField] private SpriteRenderer[] targetSpriteRenderers; // Array to hold multiple SpriteRenderers
    private bool isPhasing = false;
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource component

    void Start()
    {
        Debug.Log("SpecialPhoneBox Start method called."); // Log when Start is called

        // Ensure the target SpriteRenderers are valid
        if (targetSpriteRenderers != null && targetSpriteRenderers.Length > 0)
        {
            foreach (var spriteRenderer in targetSpriteRenderers)
            {
                if (spriteRenderer != null)
                {
                    Debug.Log($"Target SpriteRenderer assigned: {spriteRenderer.gameObject.name}"); // Log the assigned SpriteRenderer
                    Color color = spriteRenderer.color;
                    color.a = 1f; // Fully opaque
                    spriteRenderer.color = color;
                }
                else
                {
                    Debug.LogError("One of the target SpriteRenderers is not assigned in the SpecialPhoneBox script.");
                }
            }
        }
        else
        {
            Debug.LogError("Target SpriteRenderers array is empty or null in the SpecialPhoneBox script.");
        }
    }

    // Public method that other scripts can call to trigger interaction
    public void PerformAction()
    {
        Debug.Log("TriggerInteraction method called."); // Log when TriggerInteraction is called

        if (!isPhasing)
        {
            audioSource.Play(); // Play the audio when the action is triggered
            Debug.Log("Starting PhaseEffect coroutine."); // Log before starting the coroutine
            StartCoroutine(PhaseEffect());
        }
    }

    private System.Collections.IEnumerator PhaseEffect()
    {
        Debug.Log("PhaseEffect coroutine started."); // Log when the coroutine starts
        isPhasing = true;

        float fadeDuration = 0.5f; // Duration for each fade phase (fade-out and fade-in)
        int phaseCount = 3; // Number of fade-out and fade-in cycles for disappearance
        int reappearPhaseCount = 3; // Number of fade-out and fade-in cycles for reappearance

        // Disappearance phases
        for (int i = 0; i < phaseCount; i++)
        {
            // Fade out (alpha goes from 1 to 0)
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Interpolate alpha from 1 to 0
                UpdateSpriteAlpha(alpha);
                yield return null;
            }

            // Ensure fully transparent at the end of fade-out
            UpdateSpriteAlpha(0f);

            // Only fade back in if it's not the last phase
            if (i < phaseCount - 1)
            {
                // Fade in (alpha goes from 0 to 1)
                elapsedTime = 0f;
                while (elapsedTime < fadeDuration)
                {
                    elapsedTime += Time.deltaTime;
                    float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // Interpolate alpha from 0 to 1
                    UpdateSpriteAlpha(alpha);
                    yield return null;
                }

                // Ensure fully opaque at the end of fade-in
                UpdateSpriteAlpha(1f);
            }
        }

        // Make the sprite disappear for 1 second after the last phase
        yield return new WaitForSeconds(1f); // Wait for 1 second

        // Reappearance phases
        for (int i = 0; i < reappearPhaseCount; i++)
        {
            // Fade in (alpha goes from 0 to 1)
            float elapsedTime = 0f;
            while (elapsedTime < fadeDuration)
            {
                elapsedTime += Time.deltaTime;
                float alpha = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration); // Interpolate alpha from 0 to 1
                UpdateSpriteAlpha(alpha);
                yield return null;
            }

            // Ensure fully opaque at the end of fade-in
            UpdateSpriteAlpha(1f);

            // Only fade back out if it's not the last phase
            if (i < reappearPhaseCount - 1)
            {
                // Fade out (alpha goes from 1 to 0)
                elapsedTime = 0f;
                while (elapsedTime < fadeDuration)
                {
                    elapsedTime += Time.deltaTime;
                    float alpha = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration); // Interpolate alpha from 1 to 0
                    UpdateSpriteAlpha(alpha);
                    yield return null;
                }

                // Ensure fully transparent at the end of fade-out
                UpdateSpriteAlpha(0f);
            }
        }

        isPhasing = false;
        Debug.Log("PhaseEffect coroutine ended."); // Log when the coroutine ends
    }

    // Helper method to update the sprites' alpha
    private void UpdateSpriteAlpha(float alpha)
    {
        if (targetSpriteRenderers != null && targetSpriteRenderers.Length > 0)
        {
            foreach (var spriteRenderer in targetSpriteRenderers)
            {
                if (spriteRenderer != null)
                {
                    Color color = spriteRenderer.color;
                    color.a = alpha;
                    spriteRenderer.color = color;
                    Debug.Log($"Updated {spriteRenderer.gameObject.name} alpha: {alpha}"); // Log the updated alpha
                }
                else
                {
                    Debug.LogError("One of the target SpriteRenderers is null during PhaseEffect.");
                }
            }
        }
        else
        {
            Debug.LogError("Target SpriteRenderers array is empty or null during PhaseEffect.");
        }
    }
}
