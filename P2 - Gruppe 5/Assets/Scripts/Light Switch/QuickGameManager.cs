using UnityEngine;
using TMPro;  // Import TMP namespace
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class QuickTapGameManager : MonoBehaviour
{
    public TMP_Text promptText;  // Change UI Text to TMP_Text for the prompt
    public TMP_Text scoreText;   // Change UI Text to TMP_Text for score
    public Button startButton;   // UI Button to start the game
    public float gameDuration = 30f; // Time limit for the game
    private int score;
    private bool gameStarted = false;
    private bool isGamepad;
    private bool isKeyboard;

    // Reference to Input Action Asset (configured in the editor)
    public InputActionAsset inputActions;

    private InputAction tapAction;
    private InputAction switchToKeyboardAction;
    private InputAction switchToGamepadAction;

    private List<string> buttonSequence = new List<string>();  // To store the sequence of button presses
    private int currentSequenceIndex = 0; // Tracks the current index in the sequence

    void Start()
    {
        score = 0;
        scoreText.text = "Score: " + score;

        // Link the start button to start the game
        startButton.onClick.AddListener(StartGame);

        // Assign the actions from the Input Action Asset
        var gameplayMap = inputActions.FindActionMap("Gameplay");
        tapAction = gameplayMap.FindAction("Tap");
        switchToKeyboardAction = gameplayMap.FindAction("SwitchToKeyboard");
        switchToGamepadAction = gameplayMap.FindAction("SwitchToGamepad");

        // Disable actions initially
        tapAction.Disable();
        switchToKeyboardAction.Disable();
        switchToGamepadAction.Disable();
    }

    void Update()
    {
        if (gameStarted)
        {
            // Detect input device switch
            if (switchToGamepadAction.triggered && !isGamepad)
            {
                isGamepad = true;
                isKeyboard = false;
                StartNewSequence(); // Update prompt for gamepad input
            }
            else if (switchToKeyboardAction.triggered && !isKeyboard)
            {
                isKeyboard = true;
                isGamepad = false;
                StartNewSequence(); // Update prompt for keyboard input
            }

            // Check if the tap action is triggered (button/key was pressed)
            if (tapAction.triggered)
            {
                string pressedKey = GetPressedKey();

                // Check if the correct key was pressed in the sequence
                if (pressedKey == buttonSequence[currentSequenceIndex])
                {
                    currentSequenceIndex++;

                    // If the sequence is completed, score a point and reset the sequence
                    if (currentSequenceIndex >= buttonSequence.Count)
                    {
                        score++;
                        scoreText.text = "Score: " + score;

                        // Start a new sequence
                        StartNewSequence();
                    }
                }
                else
                {
                    // If the wrong button was pressed, reset the sequence
                    currentSequenceIndex = 0;
                }
            }
        }
    }

    void StartGame()
    {
        gameStarted = true;
        startButton.gameObject.SetActive(false);  // Hide start button
        score = 0;
        scoreText.text = "Score: " + score;

        // Set the input device based on current setup
        if (Gamepad.current != null)
        {
            isGamepad = true;
            isKeyboard = false;
        }
        else
        {
            isGamepad = false;
            isKeyboard = true;
        }

        // Start the sequence immediately after the game starts
        StartNewSequence();  // Start the first sequence
        tapAction.Enable();  // Enable tap action
        switchToKeyboardAction.Enable();  // Enable keyboard switch action
        switchToGamepadAction.Enable();  // Enable gamepad switch action

        StartCoroutine(GameTimer());
    }

    void StartNewSequence()
    {
        // Generate a new sequence of 5 random key presses
        buttonSequence.Clear();
        currentSequenceIndex = 0;  // Reset sequence index

        for (int i = 0; i < 5; i++)
        {
            int randomKey = Random.Range(0, 4);  // W, A, S, D or A, B, X, Y

            // Add a new prompt to the sequence
            if (isGamepad)
            {
                // Gamepad prompts
                if (randomKey == 0)
                    buttonSequence.Add("A");
                else if (randomKey == 1)
                    buttonSequence.Add("B");
                else if (randomKey == 2)
                    buttonSequence.Add("X");
                else
                    buttonSequence.Add("Y");
            }
            else if (isKeyboard)
            {
                // Keyboard prompts
                if (randomKey == 0)
                    buttonSequence.Add("W");
                else if (randomKey == 1)
                    buttonSequence.Add("A");
                else if (randomKey == 2)
                    buttonSequence.Add("S");
                else
                    buttonSequence.Add("D");
            }
        }

        // Display the prompt for the first button in the sequence
        promptText.text = "Press " + buttonSequence[currentSequenceIndex] + " to start the sequence";
    }

    IEnumerator GameTimer()
    {
        float timeRemaining = gameDuration;
        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            yield return null;
        }
        EndGame();
    }

    void EndGame()
    {
        gameStarted = false;
        tapAction.Disable();  // Disable input action
        promptText.text = "Game Over!";
        startButton.gameObject.SetActive(true);  // Show start button again
    }

    // Function to check which key/button was pressed
    private string GetPressedKey()
    {
        // Check if a specific key/button is pressed
        if (isGamepad)
        {
            // Gamepad buttons
            if (Gamepad.current.buttonSouth.isPressed) return "A";  // Gamepad A
            if (Gamepad.current.buttonEast.isPressed) return "B";   // Gamepad B
            if (Gamepad.current.buttonWest.isPressed) return "X";   // Gamepad X
            if (Gamepad.current.buttonNorth.isPressed) return "Y";   // Gamepad Y
        }
        else if (isKeyboard)
        {
            // Keyboard keys
            if (Keyboard.current.wKey.isPressed) return "W";
            if (Keyboard.current.aKey.isPressed) return "A";
            if (Keyboard.current.sKey.isPressed) return "S";
            if (Keyboard.current.dKey.isPressed) return "D";
        }

        return "";  // Return empty if no key/button is pressed
    }
}
