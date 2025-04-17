using UnityEngine;
using DialogueEditor;

public class CampPlzWinBot : MonoBehaviour
{
    private Animator animator; // Reference to the Animator component
    public NPCConversation winConversation; // Reference to the NPCConversation component
    private bool hasFinished = false; // Flag to check if the animation has finished
    [SerializeField] private MapButtonManager mapButtonManager;
    private float waitTime = 5f; // Time to wait before despawning the object and starting the game
    void Start()
    {
        // Get the Animator component attached to this GameObject
        animator = GetComponent<Animator>();

        if (animator == null)
        {
            Debug.LogError("Animator component not found on this GameObject!");
        }
        
    }

    void Update()
    {
        // Check if the animation has finished
        if (animator != null && IsAnimationFinished("WinBot"))
        {
            if (!hasFinished)
                OnAnimationFinished();
        }
    }

    // Method to check if the specified animation has finished
    private bool IsAnimationFinished(string animationName)
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        return currentState.IsName(animationName) && currentState.normalizedTime >= 1f;
    }

    // Method to trigger when the animation finishes
    private void OnAnimationFinished()
    {
        hasFinished = true; // Set the flag to true to prevent multiple calls
        Debug.Log("Animation finished! Triggering follow-up logic.");
        // Add your logic here (e.g., despawn the bot, enable the next bot, etc.)
        ConversationManager.Instance.StartConversation(winConversation);
    }
    public void GoAway()
    {

        Debug.Log("Correct Zone! The selected button corresponds to the correct area.");

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
            yield return new WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length + waitTime);
        }

        // Despawn the object (destroy or deactivate)
        mapButtonManager.ActivateFirstBot(); // Call the method to activate the first bot
        Destroy(gameObject); // Alternatively, you can use gameObject.SetActive(false);
    }
}
