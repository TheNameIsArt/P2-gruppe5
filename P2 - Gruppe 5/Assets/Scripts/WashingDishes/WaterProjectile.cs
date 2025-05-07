using UnityEngine;

// Controls movement and behavior of the water projectile
public class WaterProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float maxDistance = 20f;

    private Vector2 direction;
    private Vector3 spawnPosition;

    void OnEnable()
    {
        spawnPosition = transform.position; // Remember spawn for distance check
    }

    // Called by the player controller to set projectile's direction.
    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
    }

    void Update()
    {
        // Move projectile
        transform.position += (Vector3)direction * speed * Time.deltaTime;

        // Deactivate if too far from start
        if (Vector3.Distance(transform.position, spawnPosition) > maxDistance)
        {
            gameObject.SetActive(false);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // Example: destroy (deactivate) on hitting a plate
        if (other.CompareTag("Plate"))
        {
            // TODO: add particle effect, sound, etc.
            gameObject.SetActive(false);
        }
    }
}



