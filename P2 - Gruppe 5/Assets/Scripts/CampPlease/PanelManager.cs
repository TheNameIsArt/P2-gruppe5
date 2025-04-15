using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
public class PanelManager : MonoBehaviour
{
    [System.Serializable]
    public class PanelMapping
    {
        public string actionName; // The name of the input action (e.g., "Cancel", "Submit")
        public GameObject panel; // The corresponding PanelChooser GameObject
    }

    [Header("Panel Mappings")]
    public List<PanelMapping> panelMappings = new List<PanelMapping>(); // List of action-to-panel mappings

    public PlayerInput playerInput; // Reference to the PlayerInput component

    void Update()
    {
        if (playerInput == null) return;

        // Iterate through all panel mappings
        foreach (var mapping in panelMappings)
        {
            // Check if the action is triggered
            if (playerInput.actions[mapping.actionName].triggered)
            {
                // Reactivate the corresponding panel if it's inactive
                if (mapping.panel != null && !mapping.panel.activeSelf)
                {
                    mapping.panel.SetActive(true);
                }
            }
        }
    }
}
