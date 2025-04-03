using DialogueEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections;
public class ExampleInputManager : MonoBehaviour
{
    public InputActionAsset inputActionsAsset; // Reference to the Input Actions asset

    private InputActionMap uiActionMap;
    private float deadZone = 0.5f; // Dead zone threshold for joystick input
    private bool canNavigate = true; // Flag to ensure joystick is released before triggering again
    private bool canInteract = true; // Flag to ensure interaction delay

    private void Awake()
    {
        if (inputActionsAsset != null)
        {
            uiActionMap = inputActionsAsset.FindActionMap("UI");

            if (uiActionMap != null)
            {
                uiActionMap.FindAction("Navigate").performed += ctx => OnNavigate(ctx);
                uiActionMap.FindAction("Submit").performed += ctx => OnSubmit(ctx);
                uiActionMap.FindAction("Cancel").performed += ctx => OnCancel(ctx);
                uiActionMap.FindAction("Click").performed += ctx => OnClick(ctx); // Bind the OnClick method
            }
            else
            {
                Debug.LogError("UI Action Map not found!");
            }
        }
        else
        {
            Debug.LogError("Input Actions Asset is not assigned!");
        }
    }

    private void OnEnable()
    {
        uiActionMap?.Enable();
    }

    private void OnDisable()
    {
        uiActionMap?.Disable();
    }

    private void OnNavigate(InputAction.CallbackContext context)
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive && canInteract)
        {
            Vector2 navigation = context.ReadValue<Vector2>();

            // Apply dead zone to joystick input and check if navigation is allowed
            if (Mathf.Abs(navigation.y) > deadZone && canNavigate)
            {
                if (navigation.y > 0)
                {
                    ConversationManager.Instance.SelectPreviousOption();
                }
                else if (navigation.y < 0)
                {
                    ConversationManager.Instance.SelectNextOption();
                }

                canNavigate = false; // Prevent further navigation until joystick is released
            }
            else if (Mathf.Abs(navigation.y) <= deadZone)
            {
                canNavigate = true; // Allow navigation when joystick is released
            }
        }
    }

    private void OnSubmit(InputAction.CallbackContext context)
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive && canInteract)
        {
            if (!ConversationManager.Instance.skipAllowed)
            {
                ConversationManager.Instance.PressSelectedOption();
                //StartCoroutine(InteractionDelay());
            }
            else
            {
                ConversationManager.Instance.ScrollingText_Skip();
            }
        }
    }

    private void OnCancel(InputAction.CallbackContext context)
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive && canInteract)
        {
            ConversationManager.Instance.EndConversation();
        }
    }

    private void OnClick(InputAction.CallbackContext context)
    {
        if (ConversationManager.Instance != null && ConversationManager.Instance.IsConversationActive)
        {
            if (ConversationManager.Instance.skipAllowed)
            {
                ConversationManager.Instance.ScrollingText_Skip();
            }
        }
    }

    private IEnumerator InteractionDelay()
    {
        canInteract = false;
        yield return new WaitForSeconds(0.2f);
        canInteract = true;
    }
}



