/*using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class MouseHoldExample : MonoBehaviour, 
{
    public InputAction mouseHoldAction;
    public WireDrawer wireDrawer;

    private void OnEnable()
    {
        mouseHoldAction.Enable();
        mouseHoldAction.performed += OnMouseHoldPerformed;
        mouseHoldAction.canceled += OnMouseHoldCanceled;
    }

    private void OnDisable()
    {
        mouseHoldAction.Disable();
        mouseHoldAction.performed -= OnMouseHoldPerformed;
        mouseHoldAction.canceled -= OnMouseHoldCanceled;
    }

    private void OnMouseHoldPerformed(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse button held down");
        // Add your logic for when the mouse button is held down
        wireDrawer.OnPointerDown();
    }

    private void OnMouseHoldCanceled(InputAction.CallbackContext context)
    {
        Debug.Log("Mouse button released");
        // Add your logic for when the mouse button is released
    }
}*/