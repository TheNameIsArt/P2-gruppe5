using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;

public class EatingGamemanagerv2 : MonoBehaviour
{
    public Sprite[] foodSprites;      // Array of available food sprites
    public Image[] foodDisplays;      // Three UI Image slots to display the sequence

    private List<int> currentSequence = new List<int>();
    private int currentInputIndex = 0;  // Tracks player's progress through the sequence
    public TextMeshProUGUI feedbackText;
    void Start()
    {
        StartNewSequence();
        feedbackText.text ="Press the right sequence";
    }

    /// <summary>
    /// Generates a new random sequence and updates the UI slots.
    /// </summary>
    void StartNewSequence()
    {
        currentSequence.Clear();
        currentInputIndex = 0;

        for (int i = 0; i < foodDisplays.Length; i++)
        {
            int rnd = Random.Range(0, foodSprites.Length);
            currentSequence.Add(rnd);

            Image img = foodDisplays[i];
            img.sprite = foodSprites[rnd];
            img.enabled = true;
            img.color = Color.white;  // reset any color modifications
        }

        Debug.Log("New sequence: " + string.Join(", ", currentSequence));
    }

    /// <summary>
    /// Handles a button press by comparing to the expected sprite index.
    /// </summary>
    /// <param name="inputIndex">Index representing the pressed button (0=A,1=S,2=K,3=L)</param>
    void HandleInput(int inputIndex)
    {
        Debug.Log($"Player pressed {inputIndex} for slot {currentInputIndex}");

        if (currentSequence[currentInputIndex] == inputIndex)
        {
            OnCorrectInput();
        }
        else
        {
            OnWrongInput();
        }
    }

    /// <summary>
    /// Called when the player input matches the expected sprite.
    /// </summary>
    void OnCorrectInput()
    {
        // Hide the correctly matched slot
        foodDisplays[currentInputIndex].enabled = false;
        currentInputIndex++;

        if (currentInputIndex >= currentSequence.Count)
        {
            Debug.Log("Sequence complete! Generating new sequence...");
            Invoke ("StartNewSequence", 1.0f);
            feedbackText.text = "YAY";
        }
    }

    /// <summary>
    /// Called when the player input is incorrect.
    /// </summary>
    void OnWrongInput()
    {
        Debug.Log("Wrong input! Restarting sequence.");
        // TODO: add visual or audio feedback here
        Invoke ("StartNewSequence", 1.0f);
        feedbackText.text = "Try again buddy";
    }

    // Input callbacks (wire these up in PlayerInput's Unity Events)
    public void OnPressA(InputAction.CallbackContext context)
    {
        if (context.performed) HandleInput(0);
    }

    public void OnPressS(InputAction.CallbackContext context)
    {
        if (context.performed) HandleInput(1);
    }

    public void OnPressK(InputAction.CallbackContext context)
    {
        if (context.performed) HandleInput(2);
    }

    public void OnPressL(InputAction.CallbackContext context)
    {
        if (context.performed) HandleInput(3);
    }
}
