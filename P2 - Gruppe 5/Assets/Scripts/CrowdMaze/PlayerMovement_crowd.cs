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
    public float shadowMoveDelay = 0.1f; // Delay for shadow movement

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
        moveCount = maxMoves;
        UpdateMoveCounterUI();
        if (restartButton != null) restartButton.SetActive(false);
    }

    void Update()
    {
        if (isGameOver) return;

        // Handle shadow tail filling
        if (fillShadowAction.triggered)
        {
            canMove = false;
            StartCoroutine(FillShadowTailIncrementally());
        }

        // Handle scene restart
        if (restartAction.triggered) RestartScene();

        // Handle player movement
        if (canMove)
        {
            Vector2 inputDirection = moveAction.ReadValue<Vector2>();
            if (inputDirection != Vector2.zero)
            {
                Move(inputDirection);
            }
        }

        // Check if the player is on a GoalTile
        IsOnGoalTile();

        // Check if the player is on a TriggerTile
        IsOnTriggerTile();
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
            StartCoroutine(FillShadowTailIncrementally());
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

    public System.Collections.IEnumerator FillShadowTailIncrementally()
    {
        // Replace shadow tails with actual tails incrementally
        GameObject[] shadowTailObjects = GameObject.FindGameObjectsWithTag("Shadow"); // Ensure correct tag is used

        foreach (GameObject shadowTail in shadowTailObjects)
        {
            Vector2 position = shadowTail.transform.position;

            // Replace shadow tail with actual tail
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
                yield break;
            }

            yield return new WaitForSeconds(shadowFillSpeed);
        }

        // Check if the player is on the goal tile
        if (IsOnGoalTile())
        {
            Debug.Log("Player Wins!");
            // Add win logic here, e.g., load a win scene or display a win message
        }

        if (restartButton != null) restartButton.SetActive(true);
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

    private bool IsOnTriggerTile()
    {
        // Check if the player is on the trigger tile
        Vector2 playerPosition = rb.position;
        Collider2D hit = Physics2D.OverlapPoint(playerPosition, LayerMask.GetMask("CrowdMaze_GoalTile"));
        bool isOnTriggerTile = hit != null;

        if (isOnTriggerTile && !wasOnTriggerTile)
        {
            Debug.Log("Player is on a TriggerTile");
            wasOnTriggerTile = true;
        }
        else if (!isOnTriggerTile)
        {
            wasOnTriggerTile = false;
        }

        return isOnTriggerTile;
    }

    public void RestartScene()
    {
        // Restart the current scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
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