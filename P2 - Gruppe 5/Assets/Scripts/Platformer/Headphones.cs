using UnityEngine;

public class Headphones : MonoBehaviour
{
    public GameObject pickupEffect; // Assign the particle effect prefab in the Inspector
    public GameObject winZone; // Assign the win zone prefab in the Inspector

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            winZone.SetActive(true); // Activate the win zone
            // Spawn the particle effect at the headphones' position
            if (pickupEffect != null)
            {
                Instantiate(pickupEffect, transform.position, Quaternion.identity);
            }

            // Enable player headphone animations
            Animator playerAnimator = other.GetComponent<Animator>();
            if (playerAnimator != null)
            {
                playerAnimator.SetBool("HasHeadphones", true);
            }

            // Reduce speaker volume by tag instead of type
            GameObject[] speakerObjects = GameObject.FindGameObjectsWithTag("Speaker");
            foreach (GameObject speakerObject in speakerObjects)
            {
                Speaker speaker = speakerObject.GetComponent<Speaker>();
                if (speaker != null)
                {
                    speaker.SetVolumeModifier(0.3f);
                }
            }

            Destroy(gameObject); // Remove headphones after pickup
        }
    }
}