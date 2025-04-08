using UnityEngine;
using UnityEngine.InputSystem;


public class InputDeviceSwitcher : MonoBehaviour
{
    public InputDevice inputDevice;

    [SerializeField] private GameObject virtualMouseObject; // Reference to the virtual mouse GameObject
    [SerializeField] private GameObject realMouseObject;    // Reference to the real mouse cursor (if applicable)
    [SerializeField] private GameObject gamepadInputObject; // Reference to gamepad-specific input handling (if applicable)

    [SerializeField] private float switchDelay = 0.01f; // Delay in seconds between input device switches
    private float lastSwitchTime = 0f; // Time of the last input device switch

    private void Start()
    {
        InputSystem.onActionChange += OnActionChange;

        // Ensure all input objects are in their default states
        SetInputState(null);
    }

    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            var inputAction = (InputAction)obj;
            var lastControl = inputAction.activeControl;
            inputDevice = lastControl.device;

            // Handle the input device and enable/disable the correct input
            HandleInputDevice(inputDevice);
        }
    }

    private void HandleInputDevice(InputDevice device)
    {
        if (device != null)
        {
            // Check if enough time has passed since the last switch
            if (Time.time - lastSwitchTime < switchDelay)
            {
                return; // Ignore the input if the delay hasn't passed
            }

            lastSwitchTime = Time.time; // Update the last switch time

            if (device.name == "VirtualMouse") // Replace "VirtualMouse" with the actual name of your virtual mouse device
            {
                SetInputState(virtualMouseObject);
            }
            else if (device is Mouse)
            {
                SetInputState(realMouseObject);
            }
            else if (device is Gamepad)
            {
                SetInputState(virtualMouseObject); // Enable virtual mouse for gamepad input
            }
            else
            {
                Debug.Log($"Other input device detected: {device.displayName}");
                SetInputState(null); // Disable all inputs if the device is unrecognized
            }
        }
    }

    private void SetInputState(GameObject activeInput)
    {
        // Enable or disable the virtual mouse
        if (virtualMouseObject != null)
        {
            virtualMouseObject.SetActive(activeInput == virtualMouseObject);
        }

        // Enable or disable the real mouse
        if (realMouseObject != null)
        {
            realMouseObject.SetActive(activeInput == realMouseObject);
        }

        // Enable or disable gamepad-specific input handling
        if (gamepadInputObject != null)
        {
            gamepadInputObject.SetActive(activeInput == virtualMouseObject); // Gamepad uses virtual mouse
        }

        // Control the system cursor visibility
        if (activeInput == realMouseObject)
        {
            Cursor.visible = true; // Show the system cursor for the real mouse
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.visible = false; // Hide the system cursor for other devices
            Cursor.lockState = CursorLockMode.Locked;
        }
    }
}
