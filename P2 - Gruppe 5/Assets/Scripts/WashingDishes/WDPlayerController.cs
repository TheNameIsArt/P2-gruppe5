using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class WDPlayerController : MonoBehaviour
{
    [Header("Movement & Aiming")]
    public float moveSpeed = 5f;
    public Transform waterGun;
    public Camera mainCamera;
    public Transform firePoint;
    public float aimSmoothSpeed = 10f;

    [Header("Shooting")]
    public float fireRate = 10f; // shots per second
    private float shootCooldown = 0f;
    private bool isShootingHeld = false;
    public ProjectilePooler projectilePooler;
    public AudioSource waterGunAudio;

    [Header("Crosshair")]
    public RectTransform crosshair;
    public float crosshairDistance = 100f;
    public float fadeSpeed = 5f;

    private Vector2 movementInput;
    private Vector2 aimInput;
    private Vector2 lastAimInput = Vector2.right;
    private Rigidbody2D rb;
    private WashingDishes inputActions;
    private CanvasGroup crosshairCanvasGroup;
    private bool crosshairVisible = false;

    private enum AimMode { Mouse, Controller }
    private AimMode currentAimMode = AimMode.Mouse;

    void Awake()
    {
        inputActions = new WashingDishes();
        rb = GetComponent<Rigidbody2D>();
        crosshairCanvasGroup = crosshair.GetComponent<CanvasGroup>();
        if (waterGunAudio == null)
            waterGunAudio = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        inputActions.Player.Enable();

        inputActions.Player.Move.performed += ctx => movementInput = ctx.ReadValue<Vector2>();
        inputActions.Player.Move.canceled += ctx => movementInput = Vector2.zero;

        inputActions.Player.Look.performed += ctx =>
        {
            aimInput = ctx.ReadValue<Vector2>();
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

    void OnDisable()
    {
        inputActions.Player.Disable();
    }

    void Update()
    {
        crosshairVisible = false;

        // Switch to mouse aiming if the mouse moves
        if (Mouse.current != null && Mouse.current.delta.ReadValue().magnitude > 0.1f)
        {
            currentAimMode = AimMode.Mouse;
        }

        Aim();

        // Crosshair fade
        float targetAlpha = crosshairVisible ? 1f : 0f;
        crosshairCanvasGroup.alpha = Mathf.Lerp(crosshairCanvasGroup.alpha, targetAlpha, Time.deltaTime * fadeSpeed);

        // Auto-shoot with fireRate control
        if (isShootingHeld)
        {
            shootCooldown -= Time.deltaTime;
            if (shootCooldown <= 0f)
            {
                Shoot();
                shootCooldown = 1f / fireRate;

                // Start shooting sound
                if (waterGunAudio != null && !waterGunAudio.isPlaying)
                {
                    waterGunAudio.Play();
                }
            }
        }
        else
        {
            shootCooldown = 0f;

            // Stop sound when no longer firing
            if (waterGunAudio != null && waterGunAudio.isPlaying)
            {
                waterGunAudio.Stop();
            }
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
        if (currentAimMode == AimMode.Controller)
        {
            crosshairVisible = true;

            Vector3 rawOffset = new Vector3(lastAimInput.x, lastAimInput.y, 0);
            if (rawOffset.magnitude > 1f)
                rawOffset.Normalize();

            Vector3 aimWorldOffset = rawOffset * crosshairDistance;
            Vector3 crosshairWorldPos = transform.position + aimWorldOffset;
            Vector3 screenPos = mainCamera.WorldToScreenPoint(crosshairWorldPos);
            crosshair.position = screenPos;

            float targetAngle = Mathf.Atan2(lastAimInput.y, lastAimInput.x) * Mathf.Rad2Deg;
            Quaternion targetRotation = Quaternion.Euler(0, 0, targetAngle);
            waterGun.rotation = Quaternion.Lerp(waterGun.rotation, targetRotation, Time.deltaTime * aimSmoothSpeed);
        }
        else if (currentAimMode == AimMode.Mouse && Mouse.current != null)
        {
            crosshairVisible = true;

            Vector3 mouseWorldPos = mainCamera.ScreenToWorldPoint(Mouse.current.position.ReadValue());
            mouseWorldPos.z = 0f;

            Vector2 direction = mouseWorldPos - waterGun.position;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            waterGun.rotation = Quaternion.Euler(0f, 0f, angle);

            crosshair.position = Mouse.current.position.ReadValue();
        }
    }

    void Shoot()
    {
        if (projectilePooler == null || firePoint == null)
        {
            Debug.LogWarning("Projectile pooler or firePoint not assigned!");
            return;
        }

        GameObject projectile = projectilePooler.GetPooledProjectile();

        if (projectile != null)
        {
            projectile.transform.position = firePoint.position;
            projectile.transform.rotation = firePoint.rotation;

            // 🔀 Randomize scale here!
            float scale = Random.Range(0.8f, 1.3f); // tweak min/max as needed
            projectile.transform.localScale = new Vector3(scale, scale, 1f);

            projectile.SetActive(true);

            var proj = projectile.GetComponent<WaterProjectile>();
            if (proj != null)
            {
                proj.SetDirection(firePoint.right);
            }
        }
    }

}
