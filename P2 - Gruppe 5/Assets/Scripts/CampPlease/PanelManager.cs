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
    private InputDevice inputDevice;

    private void Awake()
    {
        InputSystem.onActionChange += OnActionChange;
    }

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

    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            var inputAction = (InputAction)obj;
            var lastControl = inputAction.activeControl;
            inputDevice = lastControl.device;

            // Check if the input device is a mouse
            if (inputDevice is Mouse)
            {
                TurnOffAllPanels();
            }

            if (inputDevice is Gamepad)
            {
                TurnOnAllPanels();
            }
        }
    }

    private void TurnOffAllPanels()
    {
        // Deactivate all panels in the panelMappings list
        foreach (var mapping in panelMappings)
        {
            if (mapping.panel != null && mapping.panel.activeSelf)
            {
                mapping.panel.SetActive(false);
            }
        }
    }
    private void TurnOnAllPanels()
    {
        // Activate all panels in the panelMappings list
        foreach (var mapping in panelMappings)
        {
            if (mapping.panel != null && !mapping.panel.activeSelf)
            {
                mapping.panel.SetActive(true);
            }
        }
    }
}
