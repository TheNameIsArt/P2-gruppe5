using UnityEngine;

public class CoordinateRestriction : MonoBehaviour
{
    void Update()
    {
        Vector3 position = transform.position;
        position.x = Mathf.Floor(position.x) + 0.5f;
        position.y = Mathf.Floor(position.y) + 0.5f;
        transform.position = position;
    }
}
