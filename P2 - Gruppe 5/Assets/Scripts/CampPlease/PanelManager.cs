using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEngine.EventSystems;
public class PanelManager : MonoBehaviour
{
    [System.Serializable]
    public class PanelMapping
    {
        public GameObject panel; // The corresponding PanelChooser GameObject
    }

    [Header("Panel Mappings")]
    public List<PanelMapping> panelMappings = new List<PanelMapping>(); // List of action-to-panel mappings

    public PlayerInput playerInput; // Reference to the PlayerInput component
    private InputDevice inputDevice;

    private bool panelsWereTurnedOff = false; // Flag to track if TurnOffAllPanels was called
    private GameObject lastSelectedPanel; // Stores the last selected panel

    private void Awake()
    {
        InputSystem.onActionChange += OnActionChange;
    }

    private void Update()
    {
        // Check if the "Cancel" action is triggered
        if (playerInput != null && playerInput.actions["Cancel"].triggered)
        {
            GoToLastSelectedPanel();
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

        // Set the flag to true
        panelsWereTurnedOff = true;
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

        // Only select the first panel if TurnOffAllPanels was called
        if (panelsWereTurnedOff)
        {
            if (panelMappings.Count > 0 && panelMappings[0].panel != null)
            {
                //EventSystem.current.SetSelectedGameObject(panelMappings[0].panel);
                lastSelectedPanel = panelMappings[0].panel; // Update the last selected panel
            }
            else
            {
                Debug.LogWarning("No panels available to select in the Event System.");
            }

            // Reset the flag
            panelsWereTurnedOff = false;
        }
    }

    private void GoToLastSelectedPanel()
    {
        // Set the last selected panel as the active panel in the Event System
        if (lastSelectedPanel != null)
        {
            EventSystem.current.SetSelectedGameObject(lastSelectedPanel);
        }
        else
        {
            Debug.LogWarning("No last selected panel to return to.");
        }
    }

    public void UpdateLastSelectedPanel(GameObject panel)
    {
        // Update the last selected panel
        lastSelectedPanel = panel;
    }
}
