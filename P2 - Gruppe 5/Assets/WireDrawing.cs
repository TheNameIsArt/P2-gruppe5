using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WireDrawing : MonoBehaviour
{
    // Arrays for start and target points
    public Transform[] startPoints;   // 4 start points
    public Transform[] targetPoints;  // 4 target points
    public LineRenderer[] lineRenderers; // 4 LineRenderers (one for each wire)
    public float connectionSpeed = 5f;

    private bool[] isDrawing; // Track if each wire is being drawn
    private Vector3[] currentLineEndPositions; // The current endpoint of the wire being drawn
    private Color[] pointColors; // Colors for each pair of points
    private int activeWireIndex = -1; // Index for the wire currently being drawn

    void Start()
    {
        isDrawing = new bool[startPoints.Length];
        currentLineEndPositions = new Vector3[startPoints.Length];
        pointColors = new Color[startPoints.Length];

        // Assign random colors to each point pair
        for (int i = 0; i < startPoints.Length; i++)
        {
            pointColors[i] = new Color(Random.value, Random.value, Random.value);
            startPoints[i].GetComponent<Renderer>().material.color = pointColors[i];
            targetPoints[i].GetComponent<Renderer>().material.color = pointColors[i];
        }
    }

    void Update()
    {
        // Check controller button (e.g., A or X) for each wire
        for (int i = 0; i < startPoints.Length; i++)
        {
            if (Input.GetButtonDown("Fire1") && !isDrawing[i])  // Replace "Fire1" with controller button
            {
                // Start drawing wire
                isDrawing[i] = true;
                activeWireIndex = i; // Set the current wire being drawn
                currentLineEndPositions[i] = startPoints[i].position;

                lineRenderers[i].positionCount = 2;
                lineRenderers[i].SetPosition(0, startPoints[i].position);
                lineRenderers[i].startColor = pointColors[i]; // Set start color for the wire
                lineRenderers[i].endColor = pointColors[i];   // Set end color for the wire (same as start)
            }

            if (isDrawing[i])
            {
                DrawWire(i);
            }

            if (Input.GetButtonUp("Fire1") && isDrawing[i])
            {
                ValidateWire(i);
                isDrawing[i] = false; // Stop drawing when button is released
                activeWireIndex = -1; // Reset the active wire index
            }
        }
    }

    private void DrawWire(int index)
    {
        // Update the wire's end position based on user input (e.g., mouse position or controller stick)
        // Here we use the mouse position for simplicity (you can change to controller input)
        Vector3 mouseWorldPos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10f));
        currentLineEndPositions[index] = mouseWorldPos;

        // Move the wire end position toward the mouse cursor (or controller input)
        lineRenderers[index].SetPosition(1, currentLineEndPositions[index]);
    }

    private void ValidateWire(int index)
    {
        // Validate if the wire connects to the correct target point by position
        float distanceToTarget = Vector3.Distance(currentLineEndPositions[index], targetPoints[index].position);

        if (distanceToTarget < 0.5f)  // You can adjust the tolerance (0.5f) based on how precise the connection should be
        {
            Debug.Log("Correct Connection for wire " + (index + 1));
        }
        else
        {
            Debug.Log("Wrong Connection for wire " + (index + 1) + ". Try Again.");
            lineRenderers[index].positionCount = 0; // Reset wire
        }
    }
}