using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WDPlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Transform waterGun;
    public Camera mainCamera;
    public GameObject waterProjectilePrefab;
    public Transform firePoint;
    public float aimSmoothSpeed = 10f;
    public RectTransform crosshair;
    public float crosshairDistance = 100f;

    private Vector2 movementInput;
    private Vector2 aimInput;
    private Rigidbody2D rb;
    private WashingDishes inputActions;
    private enum AimMode { Mouse, Controller }
    private AimMode currentAimMode = AimMode.Mouse;

    void Awake()
    {
        inputActions = new WashingDishes();
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();
        inputActions.Player.Shoot.performed += OnShootPerformed;
        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;
        inputActions.Player.Look.performed += ctx => aimInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Look.canceled += ctx => aimInput = Vector2.zero;
        inputActions.Player.Look.performed += ctx =>
        {
            aimInput = ctx.ReadValue<Vector2>();

            // Only switch if the input came from a gamepad
            if (ctx.control.device is Gamepad && aimInput.magnitude > 0.1f)
            {
                currentAimMode = AimMode.Controller;
            }
        };
    }

    void OnDisable()
    {
        inputActions.Player.Shoot.performed -= OnShootPerformed;
        inputActions.Player.Disable();
    }

    void Update()
    {
        Aim();

        if (Mouse.current != null && Mouse.current.delta.ReadValue().magnitude > 0.1f)
        {
            currentAimMode = AimMode.Mouse;
        }
    }

    void FixedUpdate()
    {
        if (rb != null)
        {
            rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
        }
    }

    void Aim()
    {
        if (currentAimMode == AimMode.Controller && aimInput.magnitude > 0.1f)
        {
            // Controller aiming
            float targetAngle = Mathf.Atan2(aimInput.y, aimInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            waterGun.rotation = Quaternion.Lerp(waterGun.rotation, targetRotation, Time.deltaTime * aimSmoothSpeed);

            Vector3 aimWorldOffset = new Vector3(aimInput.x, aimInput.y, 0).normalized * crosshairDistance;
            Vector3 screenPos = mainCamera.WorldToScreenPoint(transform.position + aimWorldOffset);
            crosshair.position = screenPos;
            crosshair.gameObject.SetActive(true);
        }
        else if (currentAimMode == AimMode.Mouse && Mouse.current != null)
        {
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPos.z = 0f;

            Vector2 direction = mouseWorldPos - waterGun.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            waterGun.rotation = Quaternion.Euler(0f, 0f, angle);

            crosshair.position = Mouse.current.position.ReadValue();
            crosshair.gameObject.SetActive(true);
        }
        else
        {
            crosshair.gameObject.SetActive(false);
        }
    }

    void OnShootPerformed(InputAction.CallbackContext context)
    {
        Shoot();
    }

    void Shoot()
    {
        if (waterProjectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Shoot() missing reference: Check prefab and firePoint!");
            return;
        }

        GameObject projectile = Instantiate(waterProjectilePrefab, firePoint.position, Quaternion.identity);
        Vector2 direction = firePoint.right;

        var proj = projectile.GetComponent<WaterProjectile>();
        if (proj != null)
        {
            proj.SetDirection(direction);
        }
    }
}