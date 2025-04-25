using UnityEngine;
using UnityEngine.Tilemaps;

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

            // Get the position of the target GameObject
            if (targetGameObject != null)
            {
                Vector3 targetPosition = targetGameObject.transform.position;
                Debug.Log($"Target GameObject position: {targetPosition}");
                playerMovement.startingPosition = targetPosition; // Set the starting position
            }
            else
            {
                Debug.LogWarning("Target GameObject is not assigned. Defaulting to (0,0).");
                playerMovement.startingPosition = Vector2.zero; // Default to (0,0) if no GameObject is assigned
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
