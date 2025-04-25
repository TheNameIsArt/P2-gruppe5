// Handles player movement and shadow tail mechanics in the game
using UnityEngine;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using System.Collections;

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
    public float shadowMoveDelay = 0.1f; // Delay for shadow movement
    public Vector2 startingPosition; // Store the player's starting position
    public GameObject followerObject; // Object that will follow the route
    public float followerSpeed = 2f; // Speed of the follower object
                                     // Add a private variable to store the coroutine reference
    private Coroutine moveFollowerCoroutine;

    // Private variables for internal logic
    private Rigidbody2D rb; // Rigidbody component for movement
    private float stepSize = 1f; // Step size for each move
    private bool canMove = true; // Flag to control movement
    private int moveCount = 0; // Counter for remaining moves
    private bool isGameOver = false; // Flag to check if the game is over
    private bool wasOnGoalTile = false; // Flag to track if player was on a goal tile
    private bool wasOnTriggerTile = false; // Flag to track if player was on a trigger tile
    private GameObject firstTriggeredTile = null; // To store the first trigger tile

    // Input actions
    private InputAction moveAction; // Action for player movement
    private InputAction fillShadowAction; // Action for filling shadow tails
    private InputAction restartAction; // Action for restarting the game

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

        if (moveAction != null) moveAction.Enable();
        if (fillShadowAction != null) fillShadowAction.Enable();
        if (restartAction != null) restartAction.Enable();
    }

    void Start()
    {
        // Initialize variables and UI
        rb = GetComponent<Rigidbody2D>();
        startingPosition = rb.position; // Store the starting position
        moveCount = maxMoves;
        UpdateMoveCounterUI();
        if (restartButton != null) restartButton.SetActive(false);

        // Set the follower object's initial position
        if (followerObject != null)
        {
            followerObject.transform.position = startingPosition;
        }
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
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            direction.y = 0;
        }
        else
        {
            direction.x = 0;
        }

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

    // Add a private variable to store the coroutine reference
    private Coroutine fillShadowTailCoroutine;

    public System.Collections.IEnumerator FillShadowTailIncrementally()
    {
        // Find all shadow tail objects in the scene
        GameObject[] shadowTailObjects = GameObject.FindGameObjectsWithTag("Shadow");

        foreach (GameObject shadowTail in shadowTailObjects)
        {
            if (shadowTail == null) continue; // Skip if the shadow tail was already destroyed

            Vector2 position = shadowTail.transform.position;

            // Move the follower object to the shadow tail's position
            if (followerObject != null)
            {
                // Stop any ongoing follower movement coroutine
                if (moveFollowerCoroutine != null)
                {
                    StopCoroutine(moveFollowerCoroutine);
                }

                // Start moving the follower to the current shadow tail position
                moveFollowerCoroutine = StartCoroutine(MoveFollowerToPosition(followerObject, position));
                yield return moveFollowerCoroutine; // Wait until the follower reaches the position
            }

            // Replace the shadow tail with an actual tail
            GameObject newTail = Instantiate(tailPrefab, position, Quaternion.identity);
            Destroy(shadowTail);

            // Check if the new tail triggers the first triggered tile
            if (firstTriggeredTile != null && (Vector2)firstTriggeredTile.transform.position == position)
            {
                // Move the player to the first triggered tile
                rb.position = position;

                // Destroy all tails
                GameObject[] allTails = GameObject.FindGameObjectsWithTag("Tail");
                foreach (GameObject tail in allTails)
                {
                    Destroy(tail);
                }

                Debug.Log("Player moved to the first triggered tile and tails cleared.");
                yield break; // Exit the coroutine
            }

            // Wait for a short delay before processing the next shadow tail
            yield return new WaitForSeconds(shadowFillSpeed);
        }

        // After processing all shadow tails, move the follower to the player's position
        if (followerObject != null)
        {
            // Stop any ongoing follower movement coroutine
            if (moveFollowerCoroutine != null)
            {
                StopCoroutine(moveFollowerCoroutine);
            }

            // Start moving the follower to the player's position
            moveFollowerCoroutine = StartCoroutine(MoveFollowerToPosition(followerObject, transform.position));
            yield return moveFollowerCoroutine; // Wait until the follower reaches the player's position
        }

        // Check if the player is on the goal tile
        if (IsOnGoalTile())
        {
            Debug.Log("Player Wins!");
            // Add win logic here, e.g., load a win scene or display a win message
        }

        // Enable the restart button if it exists
        if (restartButton != null) restartButton.SetActive(true);
    }

    // Coroutine to move the follower object to a target position
    private IEnumerator MoveFollowerToPosition(GameObject follower, Vector2 targetPosition)
    {
        while ((Vector2)follower.transform.position != targetPosition)
        {
            follower.transform.position = Vector2.MoveTowards(
                follower.transform.position,
                targetPosition,
                followerSpeed * Time.deltaTime
            );
            yield return null; // Wait for the next frame
        }

        // Clear the coroutine reference once the movement is complete
        moveFollowerCoroutine = null;
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
            Debug.Log("Coroutine stopped.");
            fillShadowTailCoroutine = null;
        }
        else
        {
            Debug.Log("Coroutine was already null. Nothing to stop.");
        }
    }
   

    private bool IsOnGoalTile()
    {
        // Check if the player is on the goal tile
        Vector2 playerPosition = rb.position;
        Collider2D hit = Physics2D.OverlapPoint(playerPosition, LayerMask.GetMask("CrowdMaze_GoalTile"));
        bool isOnGoalTile = hit != null;

        if (isOnGoalTile && !wasOnGoalTile)
        {
            Debug.Log("Player is on a GoalTile");
            wasOnGoalTile = true;
        }
        else if (!isOnGoalTile)
        {
            wasOnGoalTile = false;
        }

        return isOnGoalTile;
    }
    public void RestartScene()
    {
        // Reset the player's position to the starting position
        rb.position = startingPosition;

        // Reset the follower object's position
        if (followerObject != null)
        {
            followerObject.transform.position = startingPosition;

            // Stop the follower movement coroutine
            if (moveFollowerCoroutine != null)
            {
                StopCoroutine(moveFollowerCoroutine);
                moveFollowerCoroutine = null;
            }
        }

        // Remove all tails
        GameObject[] allTails = GameObject.FindGameObjectsWithTag("Tail");
        foreach (GameObject tail in allTails)
        {
            Destroy(tail);
        }

        // Remove all shadow tails
        GameObject[] allShadows = GameObject.FindGameObjectsWithTag("Shadow");
        foreach (GameObject shadow in allShadows)
        {
            Destroy(shadow);
        }

        // Reset move count and update the UI
        moveCount = maxMoves;
        UpdateMoveCounterUI();

        // Reset other game state variables
        isGameOver = false;
        canMove = true;
        restartButton.SetActive(false);

        // Reset the coroutine reference
        fillShadowTailCoroutine = null;

        Debug.Log("Game reset: Player position, follower position, tails, and shadows cleared.");
    }

    private Vector2 SnapToHalfCoordinates(Vector2 position)
    {
        // Snap position to half-grid coordinates
        float x = Mathf.Round(position.x * 2f) / 2f;
        float y = Mathf.Round(position.y * 2f) / 2f;
        return new Vector2(x, y);
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