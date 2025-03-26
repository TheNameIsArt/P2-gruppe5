using UnityEngine;
using System.Collections.Generic;

public class LightBeam : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform beamStart;
    public LayerMask mirrorLayer;
    public LayerMask groundLayer;  // Added ground layer
    public int maxReflections = 10;
    public float offsetDistance = 0.01f; // Adjust to avoid self-collision

    void Update()
    {
        SimulateBeam();
    }

    void SimulateBeam()
    {
        List<Vector3> beamPoints = new List<Vector3>();
        Vector3 direction = transform.right;
        Vector3 startPosition = beamStart.position;

        beamPoints.Add(startPosition);

        for (int i = 0; i < maxReflections; i++)
        {
            // Check if the beam hits either a mirror or the ground
            RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, Mathf.Infinity, mirrorLayer | groundLayer);

            if (hit.collider != null)
            {
                beamPoints.Add(hit.point);

                // If the beam hits the ground, stop further reflections
                if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
                {
                    break;  // Stop the loop if it hits the ground
                }

                // If it hits a mirror, reflect the beam
                direction = Vector2.Reflect(direction, hit.normal);

                // Offset the start position slightly to prevent re-hitting the mirror
                startPosition = hit.point + (hit.normal * offsetDistance);
            }
            else
            {
                // If no more mirrors or ground, extend the beam into the distance
                beamPoints.Add(startPosition + direction * 10f);
                break;
            }
        }

        lineRenderer.positionCount = beamPoints.Count;
        lineRenderer.SetPositions(beamPoints.ToArray());
    }
}