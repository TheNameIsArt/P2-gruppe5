using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField] private SpriteRenderer clockSpriteRenderer; // Reference to the clock's SpriteRenderer
    [SerializeField] private Sprite[] clockSprites; // Array of 17 sprites for the clock
    [SerializeField] private AudioSource audioSource; // Reference to the AudioSource component
    [SerializeField] private AudioClip timeoutSound; // Sound to play when time runs out
    [SerializeField] private Animator animator; // Reference to the Animator component
    [SerializeField] private string timeoutAnimationTrigger = "CampClock"; // Trigger name for the timeout animation

    [SerializeField] private float countdownTime = 60f; // Total countdown time in seconds
    private int totalUpdates = 17; // Total number of sprite updates
    private float updateInterval; // Time interval between sprite updates
    private int currentSpriteIndex = 0; // Tracks the current sprite index
    private float elapsedTime = 0f; // Tracks elapsed time
    private bool isCountingDown = false; // Tracks whether the countdown is active

    void Start()
    {
        // Disable the Animator to allow manual sprite updates
        if (animator != null)
        {
            animator.enabled = false;
        }

        // Calculate the interval between sprite updates
        updateInterval = countdownTime / totalUpdates;

        // Ensure the clock starts with the first sprite
        if (clockSprites.Length > 0 && clockSpriteRenderer != null)
        {
            clockSpriteRenderer.sprite = clockSprites[0];
        }
    }

    void Update()
    {
        if (!isCountingDown) return; // Exit if the countdown is not active

        // Update the countdown timer
        elapsedTime += Time.deltaTime;

        // Check if it's time to update the sprite
        if (elapsedTime >= updateInterval * (currentSpriteIndex + 1) && currentSpriteIndex < totalUpdates - 1)
        {
            UpdateClockSprite();
        }

        // Check if the countdown has finished
        if (elapsedTime >= countdownTime)
        {
            OnTimeOut();
        }
    }

    private void UpdateClockSprite()
    {
        // Update the clock sprite to the next one
        currentSpriteIndex++;
        if (currentSpriteIndex < clockSprites.Length && clockSpriteRenderer != null)
        {
            clockSpriteRenderer.sprite = clockSprites[currentSpriteIndex];
        }
    }

    private void OnTimeOut()
    {
        Debug.Log("Time's up! Triggering timeout logic.");

        // Update the clock sprite to the last one
        if (clockSprites.Length > 0 && clockSpriteRenderer != null)
        {
            clockSpriteRenderer.sprite = clockSprites[clockSprites.Length - 1];
        }

        // Play the timeout sound and trigger the animation
        if (audioSource != null && timeoutSound != null)
        {
            if (animator != null)
            {
                animator.enabled = true;
                animator.SetTrigger(timeoutAnimationTrigger);
                Debug.Log("Timeout animation triggered.");
            }

            audioSource.PlayOneShot(timeoutSound);
            Debug.Log("Timeout sound played.");

            // Start a coroutine to disable the Animator after the sound finishes
            StartCoroutine(DisableAnimatorAfterSound(timeoutSound.length));
        }
        else
        {
            Debug.LogWarning("AudioSource or TimeoutSound is not set!");
        }

        // Add your additional logic for when the time runs out
        isCountingDown = false; // Stop the countdown
        enabled = false; // Disable the script to stop further updates
        MenuScripts.Instance.GameOver(); // Call the GameOver method from MenuScripts
    }

    // Coroutine to disable the Animator after the sound finishes
    private System.Collections.IEnumerator DisableAnimatorAfterSound(float soundDuration)
    {
        // Wait for the duration of the sound
        yield return new WaitForSeconds(soundDuration);

        // Disable the Animator
        if (animator != null)
        {
            animator.enabled = false;
            Debug.Log("Animator disabled after timeout sound.");
        }
    }

    // Public method to start the countdown immediately
    public void StartCountdown()
    {
        Debug.Log("Countdown started!");

        // Reset variables
        elapsedTime = 0f;
        currentSpriteIndex = 0;
        isCountingDown = true;
        enabled = true; // Ensure the script is enabled

        // Reset the clock sprite to the first one
        if (clockSprites.Length > 0 && clockSpriteRenderer != null)
        {
            clockSpriteRenderer.sprite = clockSprites[0];
        }

        // Disable the Animator to allow manual sprite updates
        if (animator != null)
        {
            animator.enabled = false;
        }
    }

    // Public method to halve the countdown time and stop the timer
    public void HalveTimeAndStopCountdown()
    {
        Debug.Log("Reducing countdown time by 25% and stopping the timer!");

        // Reduce the countdown time by 25%
        countdownTime *= 0.75f;

        // Recalculate the interval between sprite updates
        updateInterval = countdownTime / totalUpdates;

        // Stop the countdown
        isCountingDown = false;
        Debug.Log($"Countdown time reduced to {countdownTime} seconds and stopped.");
    }
    // Public method to add 10% of the total time to the elapsed time
    public void AddTenPercentToElapsedTime()
    {
        Debug.Log("Adding 10% of the total time to the elapsed time as a punishment!");

        // Calculate 10% of the total countdown time
        float penaltyTime = countdownTime * 0.1f;

        // Add the penalty time to the elapsed time
        elapsedTime += penaltyTime;

        // Ensure elapsedTime does not exceed countdownTime
        if (elapsedTime > countdownTime)
        {
            elapsedTime = countdownTime;
            Debug.Log("Elapsed time reached the countdown limit.");
        }

        Debug.Log($"Added {penaltyTime} seconds to elapsed time. New elapsed time: {elapsedTime} seconds.");
    }
}
