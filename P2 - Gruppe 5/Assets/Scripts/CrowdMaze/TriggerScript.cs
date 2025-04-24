using UnityEngine;
using UnityEngine.Tilemaps;

public class TriggerScript : MonoBehaviour
{
    public GameObject Layer; // Object to activate
    public GameObject DespawnLayer; // Object to deactivate
    [SerializeField] private PlayerMovement_crowd playerMovement; // Reference to the PlayerMovement script
    [SerializeField] private Tilemap tilemap; // Reference to the specific tilemap

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tail")) // Check if the collider belongs to Prefab
        {
            Debug.Log("Tail collided with TriggerTile");

            // Stop the shadow tail coroutine
            playerMovement.StopFillShadowTail();

            // Get the correct world position of the tile
            Vector3Int cellPosition = tilemap.WorldToCell(transform.position); // Get the cell position
            Vector3 worldPosition = tilemap.GetCellCenterWorld(cellPosition); // Get the center of the cell in world coordinates

            // Set the starting position to the tile's world position
            Debug.Log($"Trigger cell position: {cellPosition}, world position: {worldPosition}");
            playerMovement.startingPosition = worldPosition;

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
