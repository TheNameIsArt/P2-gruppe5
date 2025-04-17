using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WDPlayerController : MonoBehaviour
{
    // Public variables for configuring player movement and aiming
    public float moveSpeed = 5f;
    public Transform waterGun;
    public Camera mainCamera;
    public GameObject waterProjectilePrefab;
    public Transform firePoint;
    public float aimSmoothSpeed = 10f;

    // Variables for crosshair behavior
    public RectTransform crosshair;
    public float crosshairDistance = 100f;
    public float fadeSpeed = 5f;

    // Private variables for handling input and movement
    private Vector2 movementInput;
    private Vector2 aimInput;
    private Vector2 lastAimInput = Vector2.right;
    private WashingDishes inputActions;
    private Rigidbody2D rb;

    // Variables for crosshair visibility
    private CanvasGroup crosshairCanvasGroup;
    private bool crosshairVisible = false;

    // Enum to differentiate between mouse and controller aiming
    private enum AimMode { Mouse, Controller }
    private AimMode currentAimMode = AimMode.Mouse;

    public float fireRate = 0.1f; // seconds between shots
    private float nextFireTime = 0f;
    private float shootCooldown = 0f;
    private bool isShootingHeld = false;

    public AudioSource waterGunAudio;


    // Awake is called when the script instance is being loaded
    void Awake()
    {
        // Initialize input actions and get references to components
        inputActions = new WashingDishes();
        rb = GetComponent<Rigidbody2D>();
        crosshairCanvasGroup = crosshair.GetComponent<CanvasGroup>();

        inputActions = new WashingDishes();
        rb = GetComponent<Rigidbody2D>();
        waterGunAudio = GetComponent<AudioSource>(); // Auto-assign if on same object
    }

    // OnEnable is called when the object becomes enabled and active
    void OnEnable()
    {
        // Enable input actions and set up input event handlers
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx =>
        {
            aimInput = ctx.ReadValue<Vector2>();

            // Switch to controller aim if input is from a gamepad
            if (ctx.control.device is Gamepad && aimInput.magnitude > 0.1f)
            {
                currentAimMode = AimMode.Controller;
                lastAimInput = aimInput;
            }
        };

        inputActions.Player.Look.canceled += ctx => aimInput = Vector2.zero;

        inputActions.Player.Shoot.performed += ctx => isShootingHeld = true;
        inputActions.Player.Shoot.canceled += ctx => isShootingHeld = false;
    }

    // OnDisable is called when the object becomes disabled and inactive
    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        crosshairVisible = false;

        // Switch to mouse aim if mouse is moved
        if (Mouse.current != null && Mouse.current.delta.ReadValue().magnitude > 0.1f)
        {
            currentAimMode = AimMode.Mouse;
        }

        Aim();

        // Smooth fade for crosshair visibility
        float targetAlpha = crosshairVisible ? 1f : 0f;
        crosshairCanvasGroup.alpha = Mathf.Lerp(crosshairCanvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);

        // Auto-shoot if holding the button
        if (isShootingHeld)
        {
            shootCooldown -= Time.deltaTime;
            if (shootCooldown <= 0f)
            {
                Shoot();
                shootCooldown = 1f / fireRate;
            }
        }
        else
        {
            shootCooldown = 0f; // reset cooldown when released
        }

        if (currentAimMode == AimMode.Controller && aimInput.magnitude > 0.1f)
        {
            // Optional: gun rotation logic
        }

        // Start firing sound
        if (inputActions.Player.Shoot.IsPressed() && Time.time >= nextFireTime)
        {
            nextFireTime = Time.time + fireRate;
            Shoot();

            if (!waterGunAudio.isPlaying)
                waterGunAudio.Play();
        }
        else if (!inputActions.Player.Shoot.IsPressed() && waterGunAudio.isPlaying)
        {
            // Stop when not firing
            waterGunAudio.Stop();
        }
    }

    // FixedUpdate is called at a fixed interval and is used for physics calculations
    void FixedUpdate()
    {
        if (rb != null)
        {
            // Move the player based on input
            rb.MovePosition(rb.position + movementInput * moveSpeed * Time.fixedDeltaTime);
        }
    }

    // Handle aiming logic based on current aim mode
    void Aim()
    {
        if (currentAimMode == AimMode.Controller)
        {
            crosshairVisible = true;

            // Calculate crosshair position based on controller input
            Vector3 rawOffset = new Vector3(lastAimInput.x, lastAimInput.y, 0);
            if (rawOffset.magnitude > 1f)
                rawOffset.Normalize();

            Vector3 aimWorldOffset = rawOffset * crosshairDistance;
            Vector3 crosshairWorldPos = transform.position + aimWorldOffset;
            Vector3 screenPos = mainCamera.WorldToScreenPoint(crosshairWorldPos);
            crosshair.position = screenPos;

            // Smoothly rotate the water gun towards the aim direction
            float targetAngle = Mathf.Atan2(lastAimInput.y, lastAimInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            waterGun.rotation = Quaternion.Lerp(waterGun.rotation, targetRotation, Time.deltaTime * aimSmoothSpeed);
        }
        else if (currentAimMode == AimMode.Mouse && Mouse.current != null)
        {
            crosshairVisible = true;

            // Calculate crosshair position based on mouse input
            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPos.z = 0f;

            Vector2 direction = mouseWorldPos - waterGun.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            waterGun.rotation = Quaternion.Euler(0f, 0f, angle);

            crosshair.position = Mouse.current.position.ReadValue();
        }
    }

    // Handle shooting logic
    void Shoot()
    {
        if (waterProjectilePrefab == null || firePoint == null)
        {
            Debug.LogWarning("Shoot() missing reference: Check prefab and firePoint!");
            return;
        }

        // Get world position of crosshair
        Vector3 crosshairWorldPos = mainCamera.ScreenToWorldPoint(crosshair.position);
        crosshairWorldPos.z = 0f;

        // Calculate direction from firePoint to crosshair
        Vector2 direction = (crosshairWorldPos - firePoint.position).normalized;

        // Instantiate and shoot the water projectile
        GameObject projectile = Instantiate(waterProjectilePrefab, firePoint.position, Quaternion.identity);

        var proj = projectile.GetComponent<WaterProjectile>();
        if (proj != null)
        {
            proj.SetDirection(direction);
        }
    }
}
