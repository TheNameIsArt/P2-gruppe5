using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject Layer; // Object to activate
    public GameObject DespawnLayer; // Object to deactivate
    [SerializeField] private PlayerMovement_crowd playerMovement; // Reference to the PlayerMovement script
    [SerializeField] private GameObject targetGameObject; // Reference to the GameObject whose position will be used

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tail")) // Check if the collider belongs to Prefab
        {
            Debug.Log("Tail collided with TriggerTile");

            // Stop the shadow tail coroutine
            playerMovement.StopFillShadowTail();

            // Update the starting position to the collider's position
            if (playerMovement != null)
            {
                playerMovement.startingPosition = other.transform.position;
                Debug.Log($"Starting position updated to: {playerMovement.startingPosition}");
            }

            // Activate and deactivate layers
            if (Layer != null)
            {
                Layer.SetActive(true);
            }

            if (DespawnLayer != null)
            {
                DespawnLayer.SetActive(false);
            }

            // Restart the scene
            playerMovement.RestartScene();

            // Destroy the trigger object
            Destroy(gameObject);
        }
    }
}
