using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class GamepadCursor : MonoBehaviour
{
    [Tooltip("Higher numbers for more mouse movement on joystick press." +
             "Warning: diagonal movement lost at lower sensitivity (<1000)")]
    public Vector2 sensitivity = new Vector2(1500f, 1500f);
    [Tooltip("Counteract tendency for cursor to move more easily in some directions")]
    public Vector2 bias = new Vector2(0f, -1f);
    public CursorScript cursorScript;
    
    public float clickRange = 50f; // Max distance to click a button

    // Cached variables
    Vector2 leftStick;
    Vector2 mousePosition;
    Vector2 warpPosition;

    // Stored for next frame
    Vector2 overflow;

    private void Start()
    {
        cursorScript = GetComponent<CursorScript>();
    }
    void Update()
    {
        if (Gamepad.current != null)
        {
            // Get the joystick position
            leftStick = Gamepad.current.leftStick.ReadValue();

            // Prevent annoying jitter when not using joystick
            if (leftStick.magnitude >= 0.1f)
            {
                // Get the current mouse position to add to the joystick movement
                mousePosition = new Vector2(Input.mousePosition.x, Input.mousePosition.y);

                // Precise value for desired cursor position, which unfortunately cannot be used directly
                warpPosition = mousePosition + bias + overflow + sensitivity * Time.deltaTime * leftStick;

                // Keep the cursor in the game screen (behavior gets weird out of bounds)
                warpPosition = new Vector2(Mathf.Clamp(warpPosition.x, 0, Screen.width), Mathf.Clamp(warpPosition.y, 0, Screen.height));

                // Store floating point values so they are not lost in WarpCursorPosition (which applies FloorToInt)
                overflow = new Vector2(warpPosition.x % 1, warpPosition.y % 1);

                // Move the cursor
                Mouse.current.WarpCursorPosition(warpPosition);
            }

            // Check if the "B" button (buttonSouth) on the gamepad is pressed
            if (Gamepad.current != null && Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                Debug.Log("B Button Pressed");
                // You can add more logic here for when the B button is pressed
                PushButton();
            }
        }
    }
    public void PushButton()
    {
        Button closestButton = FindClosestButtonInRange();
        if (closestButton != null)
        {
            closestButton.onClick.Invoke();
            Debug.Log("Object clicked button: " + closestButton.name);
        }
        else
        {
            Debug.Log("No button within range!");
        }
    }
    Button FindClosestButtonInRange()
    {
        PointerEventData pointerData = new PointerEventData(EventSystem.current)
        {
            position = Mouse.current.position.ReadValue() // Get cursor position
        };

        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(pointerData, results);

        foreach (RaycastResult result in results)
        {
            Button button = result.gameObject.GetComponent<Button>();
            if (button != null)
            {
                return button; // Return the first button found under the cursor
            }
        }

        return null; // No button found
    }
}