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
    public int sequencesCompleted = 0;
    public int sequencesToComplete = 10;
    public TextMeshProUGUI statusText;

    private bool isSequenceResetting = false; //Makes sure that the sequence only resetts once, instead of multiple times via multiple inputs
    private bool isInputProcessing = true; //Makes sure that the player cannot do a new input after a wrong input and a new sequence is being made
    
    private bool isShowingRythm = false; // When this is turned true, it makes it so that the player cannot do an input
    public float beatInterval = 0.5f; // Time between beats. Should be updated to BPM later on ?
    public AudioClip beatSound;
    private AudioSource audioSource;

   void Start()
{
    audioSource = GetComponent<AudioSource>();
    if (audioSource == null)
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    StartNewSequence();
    feedbackText.text = "Press the right sequence";
}


    void Update()
    {
        if (sequencesCompleted >= sequencesToComplete)
        {
            WinCon();
        }
    }

    /// <summary>
    /// Generates a new random sequence and updates the UI slots.
    /// </summary>
    void StartNewSequence()
    {
        isSequenceResetting = false; //Sequence can be reset again
        isInputProcessing = true;
        isShowingRythm = true;
        
        currentSequence.Clear();
        currentInputIndex = 0;

        for (int i = 0; i < foodDisplays.Length; i++)
        {
            int rnd = Random.Range(0, foodSprites.Length);
            currentSequence.Add(rnd);

            Image img = foodDisplays[i];
            img.sprite = foodSprites[rnd];
            img.enabled = true;
        }

        StartCoroutine(ShowRhythm());

    }

    
    void HandleInput(int inputIndex)
    {
        if (!isInputProcessing || isShowingRythm) return;
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
            sequencesCompleted ++;
            statusText.text = sequencesCompleted + " sequences completed!";
        }
    }

    void OnWrongInput()
    {
        if (isSequenceResetting) return; //If isSequenceResetting = true, nothing happens.
        isSequenceResetting = true;
        isInputProcessing = false; //Blocks the input 

        Debug.Log("Wrong input! Restarting sequence.");
        Invoke ("StartNewSequence", 1.0f);
        feedbackText.text = "Try again buddy";
    }

    private System.Collections.IEnumerator ShowRhythm()
{
    feedbackText.text = "Watch carefully...";

    for (int i = 0; i < currentSequence.Count; i++)
    {
        // Highlight the current food
        Image img = foodDisplays[i];
        Color originalColor = img.color;
        img.color = Color.blue; // Highlight it

        if (beatSound !=null && audioSource != null)
        {
            audioSource.PlayOneShot(beatSound);
        }

        yield return new WaitForSeconds(beatInterval);

        // Return to original color
        img.color = originalColor;

        yield return new WaitForSeconds(0.1f); // Tiny pause before next highlight
    }

    // After showing the rhythm:
    feedbackText.text = "Now it's your turn!";
    isShowingRythm = false;
    isInputProcessing = true; // Allow input
}


    void WinCon()
    {
        Debug.Log("GG, you win");
    }

    // Inputs 
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
