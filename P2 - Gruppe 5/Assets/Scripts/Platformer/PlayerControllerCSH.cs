using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerControllerCSH : MonoBehaviour
{
    private Animator animator;
    public float maxSpeed = 10f;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpImpulse = 10f;
    public float jumpHoldTime = 0.2f;
    public float jumpCutMultiplier = 0.5f;

    private float jumpTimeCounter;
    private bool isJumping;

    private Vector2 moveInput;
    private TouchingDirections touchingDirections;
    private Rigidbody2D rb;
    public bool HasHeadphones { get; private set; } = false;
    private string currentAnimation = "";

    [SerializeField]
    private bool _isMoving = false;

    public bool isMoving
    {
        get => _isMoving;
        private set => _isMoving = value;
    }

    [SerializeField]
    private bool _isRunning = false;

    public bool isRunning
    {
        get => _isRunning;
        set => _isRunning = value;
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get => _isFacingRight;
        private set
        {
            if (_isFacingRight != value)
            {
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    private float CurrentMoveSpeed
    {
        get
        {
            if (isMoving)
            {
                return isRunning ? runSpeed : walkSpeed;
            }
            else
            {
                return 0;
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    void Update()
    {
        isMoving = moveInput != Vector2.zero;
        UpdateAnimationState();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = Vector2.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        isMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isRunning = true;
        }
        else if (context.canceled)
        {
            isRunning = false;
        }
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.started && touchingDirections.IsGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpImpulse);
            isJumping = true;
            jumpTimeCounter = jumpHoldTime;
        }

        if (context.canceled && isJumping)
        {
            if (rb.linearVelocity.y > 0)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, rb.linearVelocity.y * jumpCutMultiplier);
            }
            isJumping = false;
        }
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            IsFacingRight = true;
        }
        if (moveInput.x < 0 && IsFacingRight)
        {
            IsFacingRight = false;
        }
    }

    private void UpdateAnimationState()
    {
        string newAnimation = "";

        if (!touchingDirections.IsGrounded)
        {
            newAnimation = "New_Guy_Jump";
        }
        else if (isMoving)
        {
            newAnimation = isRunning ? "New_Guy_Run" : "New_Guy_Walk";
        }
        else
        {
            newAnimation = "New_Guy_Idle";
        }

        if (newAnimation != currentAnimation)
        {
            animator.Play(newAnimation);
            currentAnimation = newAnimation;
        }
    }

   

    public void OnPickupHeadphones()
    {
        HasHeadphones = true;
        animator.Play("New_Guy_Headphones"); // Make sure this animation state exists in the Animator
    }
}