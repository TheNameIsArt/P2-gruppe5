// Handles player movement and shadow tail mechanics in the game
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlayerMovement_crowd : MonoBehaviour
{
    // Public variables for configuration
    public float moveDelay = 0.2f; // Delay between moves
    public GameObject tailPrefab; // Prefab for the tail object
    public GameObject shadowTailPrefab; // Prefab for the shadow tail object
    public TextMeshProUGUI moveCounterText; // UI element to display remaining moves
    public GameObject restartButton; // Button to restart the game
    public float shadowFillSpeed = 0.1f; // Speed of filling shadow tails
    public int maxMoves = 20; // Maximum number of moves allowed
    public Vector2 startingPosition; // Starting position of the player

    // Private variables for internal logic
    private Rigidbody2D rb; // Rigidbody component for movement
    private float stepSize = 1f; // Step size for each move
    private bool canMove = true; // Flag to control movement
    private int moveCount = 0; // Counter for remaining moves
    private bool isGameOver = false; // Flag to check if the game is over
    private bool wasOnGoalTile = false; // Flag to track if player was on a goal tile
    private GameObject firstTriggeredTile = null; // To store the first trigger tile

    // Input actions
    private InputAction moveAction; // Action for player movement
    private InputAction fillShadowAction; // Action for filling shadow tails
    private InputAction restartAction; // Action for restarting the game

    // Add a private variable to store the coroutine reference
    private Coroutine fillShadowTailCoroutine;

    void Awake()
    {
        // Initialize input actions
        var playerInput = GetComponent<PlayerInput>();
        if (playerInput == null)
        {
            Debug.LogError("PlayerInput component is missing on the GameObject.");
            return;
        }

        moveAction = playerInput.actions["Move"];
        fillShadowAction = playerInput.actions["FillShadow"];
        restartAction = playerInput.actions["Restart"];

        moveAction?.Enable();
        fillShadowAction?.Enable();
        restartAction?.Enable();
    }

    void Start()
    {
        // Initialize variables and UI
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position; // Store the starting position
        moveCount = maxMoves;
        UpdateMoveCounterUI();
        restartButton?.SetActive(false);
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Vector2 direction = context.ReadValue<Vector2>();
            Move(direction);
        }
    }

    public void OnFillShadow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            StartFillShadowTail();
            canMove = false; // Disable movement while filling shadow
        }
    }

    public void OnRestart(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            RestartScene();
        }
    }

    private void OnEnable()
    {
        // Enable input actions
        moveAction?.Enable();
        fillShadowAction?.Enable();
        restartAction?.Enable();
    }

    private void OnDisable()
    {
        // Disable input actions
        moveAction?.Disable();
        fillShadowAction?.Disable();
        restartAction?.Disable();
    }

    public void Move(Vector2 direction)
    {
        if (moveCount <= 0 || !canMove) return;

        // Restrict movement to one axis at a time
        direction = Mathf.Abs(direction.x) > Mathf.Abs(direction.y) ? new Vector2(direction.x, 0) : new Vector2(0, direction.y);

        Vector2 currentPosition = rb.position;
        Vector2 targetPosition = currentPosition + direction.normalized * stepSize;

        // Check for collision with the Wall layer
        RaycastHit2D hit = Physics2D.Raycast(currentPosition, direction, stepSize, LayerMask.GetMask("CrowdMaze_Wall"));
        if (hit.collider != null)
        {
            Debug.Log("Blocked by wall: " + hit.collider.name);
            return; // Stop movement if a wall is detected
        }

        rb.MovePosition(targetPosition);

        // Create shadow tail at the previous position with z-coordinate set to 2
        Vector2 snappedPosition = SnapToHalfCoordinates(currentPosition);
        Vector3 shadowTailPosition = new Vector3(snappedPosition.x, snappedPosition.y, 2);
        Instantiate(shadowTailPrefab, shadowTailPosition, Quaternion.identity);

        moveCount--;
        UpdateMoveCounterUI();
        StartCoroutine(MoveCooldown());
    }

    private System.Collections.IEnumerator MoveCooldown()
    {
        // Cooldown to prevent rapid movement
        canMove = false;
        yield return new WaitForSeconds(moveDelay);
        canMove = true;
    }

    public System.Collections.IEnumerator FillShadowTailIncrementally()
    {
        // Replace shadow tails with actual tails incrementally
        foreach (GameObject shadowTail in GameObject.FindGameObjectsWithTag("Shadow")) // Ensure correct tag is used
        {
            Vector2 position = shadowTail.transform.position;

            // Replace shadow tail with actual tail
            Instantiate(tailPrefab, position, Quaternion.identity);
            Destroy(shadowTail);

            // Check if the new tail triggers the first triggered tile
            if (firstTriggeredTile != null && (Vector2)firstTriggeredTile.transform.position == position)
            {
                // Move the player to the first triggered tile
                rb.position = position;

                // Destroy all tails
                // foreach (GameObject tail in GameObject.FindGameObjectsWithTag("Tail")) Destroy(tail);

                Debug.Log("Player moved to the first triggered tile and tails cleared.");
                yield break;
            }

            yield return new WaitForSeconds(shadowFillSpeed);
        }

        // Check if the player is on the goal tile
        if (IsOnGoalTile()) Debug.Log("Player Wins!");

        restartButton?.SetActive(true);
    }

    // Public method to start the coroutine
    public void StartFillShadowTail()
    {
        if (fillShadowTailCoroutine == null)
        {
            fillShadowTailCoroutine = StartCoroutine(FillShadowTailIncrementally());
        }
    }

    // Public method to stop the coroutine
    public void StopFillShadowTail()
    {
        if (fillShadowTailCoroutine != null)
        {
            StopCoroutine(fillShadowTailCoroutine);
            fillShadowTailCoroutine = null;
        }
    }

    private bool IsOnGoalTile()
    {
        // Check if the player is on the goal tile
        Vector2 playerPosition = rb.position;
        Collider2D hit = Physics2D.OverlapPoint(playerPosition, LayerMask.GetMask("CrowdMaze_GoalTile"));
        wasOnGoalTile = hit != null;
        return wasOnGoalTile;
    }

    public void RestartScene()
    {
        // Reset the player's position to the starting position
        rb.position = startingPosition;

        // Remove all tails
        foreach (GameObject tail in GameObject.FindGameObjectsWithTag("Tail")) Destroy(tail);

        // Remove all shadow tails
        foreach (GameObject shadow in GameObject.FindGameObjectsWithTag("Shadow")) Destroy(shadow);

        // Reset move count and update the UI
        moveCount = maxMoves;
        UpdateMoveCounterUI();

        // Reset other game state variables
        isGameOver = false;
        canMove = true;
        restartButton?.SetActive(false);
        // Reset the coroutine reference
        fillShadowTailCoroutine = null;

        Debug.Log("Game reset: Player position, tails, and shadows cleared.");
    }

    private Vector2 SnapToHalfCoordinates(Vector2 position)
    {
        // Snap position to half-grid coordinates
        return new Vector2(Mathf.Round(position.x * 2f) / 2f, Mathf.Round(position.y * 2f) / 2f);
    }

    private void UpdateMoveCounterUI()
    {
        // Update the move counter UI
        if (moveCounterText != null)
            moveCounterText.text = $"Moves Left: {moveCount}";
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Log collision events
        Debug.Log($"Collided with: {collision.gameObject.name}");
    }
}