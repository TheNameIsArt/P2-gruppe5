using UnityEngine;

public class PrefabPlacement : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Method to snap position to half-coordinates
    private Vector3 SnapToHalfCoordinates(Vector3 position)
    {
        return new Vector3(
            Mathf.Round(position.x * 2) / 2,
            Mathf.Round(position.y * 2) / 2,
            Mathf.Round(position.z * 2) / 2
        );
    }

    // Example method to place the prefab at a snapped position
    public void PlacePrefab(GameObject prefab, Vector3 position)
    {
        Vector3 snappedPosition = SnapToHalfCoordinates(position);

        // Check if there is already a prefab or tail at the snapped position
        Collider[] colliders = Physics.OverlapSphere(snappedPosition, 0.1f);
        foreach (Collider collider in colliders)
        {
            if (collider.gameObject.CompareTag("Prefab") || collider.gameObject.CompareTag("Tail"))
            {
                Debug.Log("Cannot place prefab: another prefab or tail is already at this position.");
                return;
            }
        }

        GameObject newPrefab = Instantiate(prefab, snappedPosition, Quaternion.identity);
        newPrefab.tag = "Prefab"; // Assign the "Prefab" tag to the instantiated prefab
    }

    // Method triggered when a trigger collision occurs
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }
}
