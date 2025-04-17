using UnityEngine;

public class campBotScript : MonoBehaviour
{
    public MapButtonManager mapButtonManager; // Reference to the MapButtonManager script
    private Animator animator; // Reference to the Animator component
    [SerializeField] private string botCorrectArea; // Correct area for the bot
    [SerializeField] private GameObject speechBubble; // Reference to the speech bubble GameObject

    void Start()
    {
        mapButtonManager.correctArea = botCorrectArea; // Set the correct area for the map button
        animator = GetComponent<Animator>(); // Get the Animator component attached to this GameObject
        
    }

    // Method to handle correct match
    public void CorrectZone()
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
