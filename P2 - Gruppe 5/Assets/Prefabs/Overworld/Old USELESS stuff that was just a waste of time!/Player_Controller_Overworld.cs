using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class Player_Controller_Overworld : MonoBehaviour
{
    public Rigidbody2D rb;
    public Animator animator;
    public int moveSpeed;
    private Vector2 playerMovement;
    private bool isFacingRight;
    private PlayerInput playerInput;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        playerInput = GetComponent<PlayerInput>();
        playerInput.onActionTriggered += Flip;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Here:" + playerInput);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        playerMovement = context.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = playerMovement * moveSpeed;
    }

    private void Flip(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            isFacingRight = !isFacingRight;
            transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
            Debug.Log("Flip");
        }

    }
}