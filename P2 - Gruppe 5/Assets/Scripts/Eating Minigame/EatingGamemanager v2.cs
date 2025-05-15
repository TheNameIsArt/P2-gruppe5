using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DialogueEditor;
public class EatingGamemanagerv2 : MonoBehaviour
{
    public Sprite[] foodSprites;      // Array of available food sprites
    public Image[] foodDisplays;      // Three UI Image slots to display the sequence
    public Image[] beatIndicators; //Three UI Images holding the indicators for the beat

    private List<int> currentSequence = new List<int>();
    private int currentInputIndex = 0;  // Tracks player's progress through the sequence
    public TextMeshProUGUI feedbackText;
    public int sequencesCompleted = 0;
    public int sequencesToComplete = 10;
    public TextMeshProUGUI statusText;

    private bool isSequenceResetting = false; //Makes sure that the sequence only resetts once, instead of multiple times via multiple inputs
    private bool isInputProcessing = true; //Makes sure that the player cannot do a new input after a wrong input and a new sequence is being made
    
    private bool isShowingRythm = false; // When this is turned true, it makes it so that the player cannot do an input
    private float beatInterval = 0.8f; // Time between beats. Should be updated to BPM later on ?
    private float beatPulseDuration = 0.3f;

    private List<float> expectedInputTiming = new List<float>();
    private float timingWindow = 0.4f;
    
    public AudioClip beatSound;
    private AudioSource audioSource;

    private bool gameStarted = false;
    public bool dialogueStartGame = false;

    [SerializeField] private PlayerInput playerInput;

   void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        //StartNewSequence();
        feedbackText.text = "Press the right sequence";
    }

    public void GameStart()
    {
        dialogueStartGame = true;
    }


    void Update()
    {
        if (ConversationManager.Instance.IsConversationActive && dialogueStartGame && !gameStarted)
            return;
        else if (!ConversationManager.Instance.IsConversationActive && !gameStarted)
        {
            gameStarted = true;
            playerInput.enabled = true;
            StartNewSequence();
        }
        if (sequencesCompleted >= sequencesToComplete)
        {
            WinCon();
        }
    }

    
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

        float inputTime = Time.time;

        // Safety check: make sure we have an expected time for this index
        if (currentInputIndex >= expectedInputTiming.Count)
        {
            Debug.LogWarning("No expected input time recorded for this index.");
            OnWrongInput();
            return;
        }

        float expectedTime = expectedInputTiming[currentInputIndex];
        float timeDifference = Mathf.Abs(inputTime - expectedTime);

        Debug.Log($"Input {inputIndex}, Expected: {currentSequence[currentInputIndex]}, TimeDiff: {timeDifference:F2}s");

        if (timeDifference <= timingWindow)
        {
            if (currentSequence[currentInputIndex] == inputIndex)
            {
                OnCorrectInput();
            }
            else
            {
                feedbackText.text = "Wrong food!";
                OnWrongInput();
            }
        }
        else
        {
            feedbackText.text = "Off-beat!";
            OnWrongInput();
        }
    }




   
    void OnCorrectInput()
    {
        if (beatSound !=null && audioSource != null)
        {
            audioSource.PlayOneShot(beatSound);
        }

        // Hide the correctly matched slot
        foodDisplays[currentInputIndex].enabled = false;
        currentInputIndex++;

        if (currentInputIndex >= currentSequence.Count)
        {
            Debug.Log("Sequence complete! Generating new sequence...");
            Invoke ("StartNewSequence", 2.0f);
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
        Invoke ("StartNewSequence", 3.0f);
        feedbackText.text = "Try again buddy";
    }

    private IEnumerator ShowRhythm()
    {
        feedbackText.text = "Watch carefully...";
        expectedInputTiming.Clear();

        float startTime = Time.time;

        for (int i = 0; i < currentSequence.Count; i++)
        {
            expectedInputTiming.Add(startTime + i * (beatInterval + 0.1f));
         

            if (beatSound !=null && audioSource != null)
            {
                audioSource.PlayOneShot(beatSound);
            }
            StartCoroutine(BeatPulse(i, 0f));

            yield return new WaitForSeconds(beatInterval);

          

            yield return new WaitForSeconds(0.1f); // Tiny pause before next highlight
        }

        // After showing the rhythm:
        feedbackText.text = "Now it's your turn!";
        isShowingRythm = false;
        isInputProcessing = true; // Allow input

        SetExpectedInputTimes();
        StartCoroutine(BeatPulseInput());
    }

    void SetExpectedInputTimes()
    {
        expectedInputTiming.Clear();
        float startTime = Time.time;

        for (int i = 0; i < currentSequence.Count; i++)
        {
            expectedInputTiming.Add(startTime + i * beatInterval);
        }
    }

   private IEnumerator BeatPulse(int index, float delay, Color? overrideColor = null)
{
    yield return new WaitForSeconds(delay); // Wait before pulsing

    Image pulse = beatIndicators[index];
    float duration = beatPulseDuration;
    float time = 0f;

    Color originalColor = pulse.color;
    Color pulseColor = overrideColor ?? originalColor;

    // Reset visual
    pulse.transform.localScale = Vector3.zero;
    pulse.color = new Color(pulseColor.r, pulseColor.g, pulseColor.b, 1f); // Fully visible

    // Scale up
    while (time < duration)
    {
        time += Time.deltaTime;
        float scale = Mathf.Lerp(0f, 1f, time / duration);
        pulse.transform.localScale = new Vector3(scale, scale, scale);
        yield return null;
    }

    // Fade out
    time = 0f;
    while (time < duration)
    {
        time += Time.deltaTime;
        float alpha = Mathf.Lerp(1f, 0f, time / duration);
        pulse.color = new Color(pulseColor.r, pulseColor.g, pulseColor.b, alpha);
        yield return null;
    }

    pulse.transform.localScale = Vector3.zero;
    pulse.color = originalColor; // Restore to red (or whatever was set in Unity)
}


    private IEnumerator BeatPulseInput()
{
    float startTime = Time.time;

    for (int i = 0; i < currentSequence.Count; i++)
    {
        float waitTime = expectedInputTiming[i] - Time.time;
        if (waitTime > 0)
            yield return new WaitForSeconds(waitTime);

        StartCoroutine(BeatPulse(i, 0f, Color.green));
    }
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
