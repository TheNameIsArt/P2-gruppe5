using UnityEngine;
using UnityEngine.InputSystem;

public class PoweredWireBehavior : MonoBehaviour
{
    public PoweredWireStats powerWireS;
    private int zvalue = 9;
    private Camera mainCamera;

    private InputAction leftMouseButtonAction;
    private bool isDragging = false;

    private void Awake()
    {
        // Assuming you have an Input Action Asset, this binds to your mouse input
        var inputActionAsset = new InputActionAsset();
        leftMouseButtonAction = inputActionAsset.FindAction("LeftMouseButton"); // Replace with your action name

        // Bind to mouse input events
        leftMouseButtonAction.started += OnMouseInput;
        leftMouseButtonAction.canceled += OnMouseInput;
    }

    private void OnEnable()
    {
        leftMouseButtonAction.Enable();
    }

    private void OnDisable()
    {
        leftMouseButtonAction.Disable();
    }

    private void Start()
    {
        powerWireS = GetComponent<PoweredWireStats>();
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MoveWire();
    }

    private void OnMouseInput(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isDragging = true;
            powerWireS.movable = true;
        }
        else if (context.canceled)
        {
            isDragging = false;
            powerWireS.movable = false;
            transform.position = powerWireS.startPosition;
        }
    }

    private void MoveWire()
    {
        if (isDragging && powerWireS.movable)
        {
            // Use virtual mouse position from the Input System
            Vector2 virtualMousePosition = Mouse.current.position.ReadValue();
            transform.position = mainCamera.ScreenToWorldPoint(new Vector3(virtualMousePosition.x, virtualMousePosition.y, zvalue));
        }
    }
}