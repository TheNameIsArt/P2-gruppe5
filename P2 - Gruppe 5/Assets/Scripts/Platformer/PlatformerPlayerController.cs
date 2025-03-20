using UnityEngine;
using UnityEngine.InputSystem;
public class PlatformerPlayerController : MonoBehaviour
{
    // Reference to the generated Input Actions class
    private PlatformerPlayerAction playerInput;

    // Rigidbody2D for physics-based movement
    private Rigidbody2D rb;

    // Animator for handling animations
    private Animator animator;

    [Header("Movement Settings")] // Shows a header in the Inspector
    public float walkSpeed = 4f;   // Speed while walking
    public float runSpeed = 8f;    // Speed while running
    public float jumpForce = 12f;  // Force applied when jumping

    private Vector2 moveInput; // Stores player movement input (X direction)
    private bool isRunning;    // True when the player is running
    private bool isGrounded;   // True when the player is on the ground

    [Header("Ground Check")] // Shows a header in the Inspector
    public Transform groundCheck; // A point at the player's feet to check if they are grounded
    public LayerMask groundLayer; // Layer mask to identify what is considered "ground"

    private void Awake()
    {
        // Initialize the PlayerInputActions instance
        playerInput = new PlatformerPlayerAction();

        // Get references to required components
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void OnEnable()
    {
        // Listen for movement input and store the value
        playerInput.PlayerAction.Move.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        playerInput.PlayerAction.Move.canceled += ctx => moveInput = Vector2.zero; // Reset movement when no input

        // Listen for run input (press and release)
        playerInput.PlayerAction.Run.performed += ctx => isRunning = true;
        playerInput.PlayerAction.Run.canceled += ctx => isRunning = false;

        // Listen for jump input and call the Jump() function
        playerInput.PlayerAction.Jump.performed += _ => Jump();

        // Enable the input actions
        playerInput.Enable();
    }

    private void OnDisable()
    {
        // Disable input actions when the object is disabled
        playerInput.Disable();
    }

    private void Update()
    {
        Move();           // Handle movement logic
        CheckGround();    // Check if the player is grounded
        UpdateAnimations(); // Update animations based on movement state
    }

    public void Move()
    {
        // Choose between walking or running speed
        float moveSpeed = isRunning ? runSpeed : walkSpeed;

        // Apply horizontal movement while keeping the current vertical velocity
        rb.linearVelocity = new Vector2(moveInput.x * moveSpeed, rb.linearVelocity.y);

        // Flip the sprite direction based on movement direction
        if (moveInput.x > 0)
            transform.localScale = new Vector3(1, 1, 1); // Facing right
        else if (moveInput.x < 0)
            transform.localScale = new Vector3(-1, 1, 1); // Facing left
    }

    public void Jump()
    {
        // Only jump if the player is grounded
        if (isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);
        }
    }

    private void CheckGround()
    {
        // Check if the player's GroundCheck object is overlapping with the ground layer
        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 1.1f, groundLayer);
    }

    private void UpdateAnimations()
    {
        // Set animator parameters based on player movement
        animator.SetFloat("Speed", Mathf.Abs(rb.linearVelocity.x)); // Speed for walk/run animations
        animator.SetBool("IsRunning", isRunning); // Running animation toggle
        animator.SetBool("IsGrounded", isGrounded); // Jump/Fall animation toggle
    }
}
