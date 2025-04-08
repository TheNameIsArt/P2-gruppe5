using UnityEngine;

public class Headphones : MonoBehaviour
{
    public GameObject pickupEffect; // Assign the particle effect prefab in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            // Spawn the particle effect at the headphones' position
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // Inform PlayerControllerCSH directly (if it has a headphone reaction)
            PlayerControllerCSH playerController = other.GetComponent<PlayerControllerCSH>();
            if (playerController != null)
            {
                playerController.OnPickupHeadphones(); // Trigger a method to handle headphone logic
            }

            // Reduce speaker volume
            Speaker[] speakers = FindObjectsOfType<Speaker>();
            foreach (Speaker speaker in speakers)
            {
                speaker.SetVolumeModifier(0.3f);
            }

            Destroy(gameObject); // Remove headphones after pickup
        }
    }
}