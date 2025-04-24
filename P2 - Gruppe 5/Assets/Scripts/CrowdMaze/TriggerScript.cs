using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject Layer; // Object to activate
    public GameObject DespawnLayer; // Object to deactivate
    [SerializeField] private PlayerMovement_crowd playerMovement; // Reference to the PlayerMovement script

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tail")) // Check if the collider belongs to Prefab
        {
            Debug.Log("Tail collided with TriggerTile");
            playerMovement.StopFillShadowTail();
            playerMovement.startingPosition = transform.position; // Set the starting position to the trigger's position
            Debug.Log("StopFillShadowTail called");

            if (Layer != null)
            {
                Layer.SetActive(true); // Activate the Layer object
            }

            if (DespawnLayer != null)
            {
                DespawnLayer.SetActive(false); // Deactivate the DespawnLayer object
            }
            playerMovement.RestartScene();
            Destroy(gameObject); // Remove the trigger object
        }
    }
}
