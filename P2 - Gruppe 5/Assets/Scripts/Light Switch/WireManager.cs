using System.Collections.Generic;
using UnityEngine;

public class WireManager : MonoBehaviour
{
    public GameObject[] startPoints;
    public GameObject[] endPoints;
    private Dictionary<GameObject, GameObject> correctPairs = new Dictionary<GameObject, GameObject>();
    private Dictionary<GameObject, GameObject> activeConnections = new Dictionary<GameObject, GameObject>(); // Tracks player connections
    public SpriteRenderer winLight;

    void Start()
    {
        if (startPoints.Length != endPoints.Length)
        {
            Debug.LogError("❌ Start and End points count mismatch! Check your setup.");
            return;
        }

        // Define correct wire connections
        for (int i = 0; i < startPoints.Length; i++)
        {
            correctPairs[startPoints[i]] = endPoints[i];
        }

        Debug.Log($" WireManager initialized with {startPoints.Length} wires.");
    }

    // Called when a wire is drawn
    public void RegisterWire(GameObject start, GameObject end)
    {
        if (!correctPairs.ContainsKey(start))
        {
            Debug.LogError($" {start.name} is NOT a valid start point!");
            return;
        }

        activeConnections[start] = end;
        Debug.Log($" Wire connected: {start.name} -> {end.name}");

        // Check win condition every time a wire is placed
        CheckWinCondition();
    }

    // Check if all wires are correctly connected
    public void CheckWinCondition()
    {
        if (activeConnections.Count != correctPairs.Count)
        {
            Debug.Log($" {activeConnections.Count}/{correctPairs.Count} wires connected.");
            return;
        }

        foreach (var pair in correctPairs)
        {
            if (!activeConnections.ContainsKey(pair.Key) || activeConnections[pair.Key] != pair.Value)
            {
                Debug.Log($" Incorrect connection: {pair.Key.name} -> {activeConnections[pair.Key]?.name ?? "NULL"} (Expected {pair.Value.name})");
                return;
            }
        }

        Debug.Log(" ALL WIRES CORRECT! YOU WIN!");
        if (winLight != null)
        {
            winLight.color = Color.yellow;
        }
    }
}