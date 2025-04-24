using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public GameObject splashEffectPrefab;

    private Vector2 direction;

    public void SetDirection(Vector2 dir)
    {
        direction = dir.normalized;
        Destroy(gameObject, lifetime);
    }

    void OnDestroy()
    {
        if (splashEffectPrefab != null)
        {
            Instantiate(splashEffectPrefab, transform.position, Quaternion.identity);
        }
    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        DirtyPlate plate = other.GetComponent<DirtyPlate>();
        if (plate != null)
        {
            plate.Clean(0.3f); // Adjust cleaning power here
            Destroy(gameObject); // One-shot water projectile
        }
    }

}

