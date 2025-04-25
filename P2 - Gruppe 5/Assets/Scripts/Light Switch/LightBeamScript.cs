using UnityEngine;
using System.Collections.Generic;

public class LightBeam : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Transform beamStart;
    public LayerMask mirrorLayer;
    public LayerMask groundLayer;
    public LayerMask winLayer;  // Added win trigger layer
    public int maxReflections = 18;
    public float offsetDistance = 0.01f; // Prevents self-collision
    private bool isWin = false;

    public delegate void OnWinCondition(); // Event for winning
    public static event OnWinCondition WinEvent;

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
            RaycastHit2D hit = Physics2D.Raycast(startPosition, direction, Mathf.Infinity, mirrorLayer | groundLayer | winLayer);

            if (hit.collider != null)
            {
                beamPoints.Add(hit.point);

                // Check if the beam hit the win trigger
                if (((1 << hit.collider.gameObject.layer) & winLayer) != 0)
                {
                    if (isWin) return; // Prevent multiple win events
                    Debug.Log("Win condition met!");
                    isWin = true;
                    WinEvent?.Invoke(); // Trigger the win event
                    break;
                }

                // Stop if it hits the ground
                if (((1 << hit.collider.gameObject.layer) & groundLayer) != 0)
                {
                    break;
                }

                // Reflect if it hits a mirror
                direction = Vector2.Reflect(direction, hit.normal);
                startPosition = hit.point + (hit.normal * offsetDistance);
            }
            else
            {
                // Extend the beam if no mirrors or win trigger are hit
                beamPoints.Add(startPosition + direction * 10f);
                break;
            }
        }

        lineRenderer.positionCount = beamPoints.Count;
        lineRenderer.SetPositions(beamPoints.ToArray());
    }
}