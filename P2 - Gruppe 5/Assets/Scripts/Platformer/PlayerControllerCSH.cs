using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections))]
public class PlayerControllerCSH : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float walkSpeed = 5f;
    public float runSpeed = 8f;
    public float jumpImpulse = 10f;
    public float jumpHoldTime = 0.2f; // Maximum time allowed for higher jumps
    public float jumpCutMultiplier = 0.5f; // How much to reduce velocity when releasing early
    private float jumpTimeCounter;
    private bool isJumping;
    Vector2 moveInput;
    TouchingDirections touchingDirections;

    public float CurrentMoveSpeed
    {
        get
        {
            if (isMoving)
            {
                if (isRunning)
                {
                    return runSpeed;
                }
                else
                {
                    return walkSpeed;
                }
            }
            else
            {   // Idle speed is 0
                return 0;
            }
        }
    }

    [SerializeField]
    private bool _isMoving = false;


    public bool isMoving
    {
        get
        {
            return _isMoving;
        }
        private set
        {
            animator.SetBool(PlatformerAnimationStrings.isMoving, value);
        }
    }
    [SerializeField]
    private bool _isRunning = false;

    public bool isRunning
    {
        get
        {
            return _isRunning;
        }
        set
        {
            _isRunning = value; // Update the private variable here
            animator.SetBool(PlatformerAnimationStrings.isRunning, value);
        }
    }

    public bool _isFacingRight = true;

    public bool IsFacingRight
    {
        get { return _isFacingRight; }
        private set
        {
            if (_isFacingRight != value)
            {
                //Flip local scale to make player face opposite direction
                transform.localScale *= new Vector2(-1, 1);
            }
            _isFacingRight = value;
        }
    }

    Rigidbody2D rb;
    Animator animator;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    void Update()
    {
        isMoving = moveInput != Vector2.zero; // Update the moving animation state
    }

    private void FixedUpdate()
    {

        rb.linearVelocity = new Vector2(moveInput.x * CurrentMoveSpeed, rb.linearVelocity.y);

        animator.SetFloat(PlatformerAnimationStrings.yVelocity, rb.linearVelocity.y);

        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity = Vector3.ClampMagnitude(rb.linearVelocity, maxSpeed);
        }
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();

        _isMoving = moveInput != Vector2.zero;

        SetFacingDirection(moveInput);
    }

    private void SetFacingDirection(Vector2 moveInput)
    {
        if (moveInput.x > 0 && !IsFacingRight)
        {
            //Face Right
            IsFacingRight = true;
        }
        if (moveInput.x < 0 && IsFacingRight)
        {
            //Face Left
            IsFacingRight = false;
        }
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
            animator.SetTrigger(PlatformerAnimationStrings.jump);
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
}
