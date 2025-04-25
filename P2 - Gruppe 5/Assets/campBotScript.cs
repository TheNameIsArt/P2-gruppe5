using UnityEngine;

public class campBotScript : MonoBehaviour
{
    public MapButtonManager mapButtonManager; // Reference to the MapButtonManager script
    private Animator animator; // Reference to the Animator component
    [SerializeField] private string botCorrectArea; // Correct area for the bot
    [SerializeField] private GameObject speechBubble; // Reference to the speech bubble GameObject
    [SerializeField] private TimeManager timeManager; // Reference to the TimeManager script
    [SerializeField] private string initialAnimationName = "CampBots"; // Name of the initial animation

    void Start()
    {
        mapButtonManager.correctArea = botCorrectArea; // Set the correct area for the map button
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
        

        if (timeManager == null)
        {
            //Debug.LogError("TimeManager is not assigned in the Inspector!");
        }

        // Start a coroutine to wait for the current animation to finish
        StartCoroutine(WaitForAnimationToFinish());
    }

    private System.Collections.IEnumerator WaitForAnimationToFinish()
    {
        if (animator != null)
        {
            // Wait until the current animation finishes
            while (IsAnimationPlaying(initialAnimationName))
            {
                yield return null; // Wait for the next frame
            }
        }

        // Start the countdown in the TimeManager
        if (timeManager != null)
        {
            timeManager.StartCountdown();
        }
    }

    private bool IsAnimationPlaying(string animationName)
    {
        if (animator != null)
        {
            AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
            return currentState.IsName(animationName) && currentState.normalizedTime < 1f;
        }
        return false;
    }

    // Method to handle correct match
    public void CorrectZone()
    {
        Debug.Log("Correct Zone! The selected button corresponds to the correct area.");
        timeManager.HalveTimeAndStopCountdown(); // Stop the countdown in the TimeManager

        // Play the win animation
        if (animator != null)
        {
            animator.SetTrigger("CampPlzWin"); // Trigger the "Win" animation
        }

        // Start a coroutine to despawn the object after the animation finishes
        StartCoroutine(DespawnAfterAnimation());
    }

    // Coroutine to despawn the object after the animation finishes
    private System.Collections.IEnumerator DespawnAfterAnimation()
    {
        if (animator != null)
        {
            // Wait for the length of the "Win" animation
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        }

        // Despawn the object (destroy or deactivate)
        Destroy(gameObject); // Alternatively, you can use gameObject.SetActive(false);
    }

    public void SpeechBubleApear()
    {
        speechBubble.SetActive(true); // Show the speech bubble
    }
    public void SpeechBubleDisapear()
    {
        speechBubble.SetActive(false); // Hide the speech bubble
    }
}
