using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    public List<Transform> buttons = new List<Transform>(); // Store all available buttons

    // A structure to represent a Wire
    [System.Serializable]
    public class Wire
    {
        public Transform startButton;  // The start button of the wire
        public Transform endButton;    // The end button of the wire

        public Wire(Transform start, Transform end)
        {
            startButton = start;
            endButton = end;
        }
    }

    private List<Wire> wires = new List<Wire>(); // List to store all wires

    // Method to add a wire to the manager
    public void AddWire(Vector3 startPosition, Vector3 endPosition)
    {
        // Find the closest start and end buttons to the given positions
        Transform closestStart = GetClosestButton(startPosition);
        Transform closestEnd = GetClosestButton(endPosition);

        // Ensure we're not connecting the same button
        if (closestStart == closestEnd) return;

        // Create a new Wire object and add it to the list
        Wire newWire = new Wire(closestStart, closestEnd);
        wires.Add(newWire);

        Debug.Log($"Wire added between {closestStart.name} and {closestEnd.name}");
    }

    // Method to get the closest button to a given position
    private Transform GetClosestButton(Vector3 position)
    {
        Transform closestButton = null;
        float minDistance = Mathf.Infinity;

        foreach (Transform button in buttons)
        {
            float distance = Vector3.Distance(position, button.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closestButton = button;
            }
        }

        return closestButton;
    }

    // Optional: Clear all wires
    public void ClearWires()
    {
        wires.Clear();
        Debug.Log("All wires have been cleared.");
    }

    // Optional: Check if all wires are correctly connected
    public bool AreAllWiresConnected()
    {
        // Implement logic to check if the correct wires are connected
        return true;
    }
}