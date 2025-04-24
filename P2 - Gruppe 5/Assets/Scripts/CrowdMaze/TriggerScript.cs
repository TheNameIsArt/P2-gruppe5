using UnityEngine;

public class TriggerScript : MonoBehaviour
{
    public GameObject Layer; // Object to activate
    public GameObject DespawnLayer; // Object to deactivate

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Prefab")) // Check if the collider belongs to Prefab
        {
            Debug.Log("Tail collided with TriggerTile");

            if (Layer != null)
            {
                Layer.SetActive(true); // Activate the Layer object
            }

            if (DespawnLayer != null)
            {
                DespawnLayer.SetActive(false); // Deactivate the DespawnLayer object
            }

            Destroy(gameObject); // Remove the trigger object
        }
    }
}
