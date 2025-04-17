using UnityEngine;

public class WaterProjectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;
    public GameObject splashEffectPrefab;

    public float maxDistanceFromCamera = 20f; // Tune as needed
    private Camera mainCamera;


    private Vector2 direction;


    private void Awake()
    {
        mainCamera = Camera.main;
    }
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

        if (mainCamera == null) return;

        Vector3 screenPos = mainCamera.WorldToViewportPoint(transform.position);

        bool offScreen = screenPos.x < -0.2f || screenPos.x > 1.2f ||
                         screenPos.y < -0.2f || screenPos.y > 1.2f;

        if (offScreen)
        {
            Destroy(gameObject);
        }
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

