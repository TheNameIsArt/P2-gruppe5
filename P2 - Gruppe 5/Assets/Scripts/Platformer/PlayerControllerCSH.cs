using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D), typeof(TouchingDirections), typeof(Animator))]
public class PlayerControllerCSH : MonoBehaviour
{
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
    private Animator animator;

    public bool HasHeadphones { get; private set; } = false;

    private bool _isMoving;
    private bool _isRunning;
    private bool _isFacingRight = true;
    private string currentState = "";

    private float CurrentMoveSpeed => _isMoving ? (_isRunning ? runSpeed : walkSpeed) : 0;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        touchingDirections = GetComponent<TouchingDirections>();
    }

    private void Update()
    {
        _isMoving = moveInput != Vector2.zero;
        UpdateAnimation();
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
        _isMoving = moveInput != Vector2.zero;
        SetFacingDirection(moveInput);
    }

    public void OnRun(InputAction.CallbackContext context)
    {
        _isRunning = context.ReadValue<float>() > 0.5f;
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
        if (moveInput.x > 0 && !_isFacingRight)
        {
            Flip();
        }
        else if (moveInput.x < 0 && _isFacingRight)
        {
            Flip();
        }
    }

    private void Flip()
    {
        _isFacingRight = !_isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    private void UpdateAnimation()
    {
        string newState = "";

        float yVelocity = rb.linearVelocity.y;

        if (!touchingDirections.IsGrounded)
        {
            if (yVelocity > 1f)
                newState = HasHeadphones ? "Delilah_P_H_Jump" : "Delilah_P_Jump";
            else if (yVelocity > 0f)
                newState = HasHeadphones ? "Delilah_P_H_Jump" : "Delilah_P_Jump";
            else
                newState = HasHeadphones ? "Delilah_P_H_Fall" : "Delilah_P_Fall";
        }
        else if (_isMoving)
        {
            if (_isRunning)
                newState = HasHeadphones ? "Delilah_P_H_Run" : "Delilah_P_Run";
            else
                newState = HasHeadphones ? "Delilah_P_H_Run" : "Delilah_P_Run";
        }
        else
        {
            newState = HasHeadphones ? "Delilah_P_H_Idle" : "Delilah_P_Idle";
        }

        if (newState != currentState)
        {
            animator.Play(newState);
            currentState = newState;
        }
    }

    public void OnPickupHeadphones()
    {
        HasHeadphones = true;
    }
}

