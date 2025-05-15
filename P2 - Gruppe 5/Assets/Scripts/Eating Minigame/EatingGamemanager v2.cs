using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using TMPro;
using System.Collections;
using DialogueEditor;

public class EatingGamemanagerv2 : MonoBehaviour
{
    public Sprite[] foodSprites;
    public Image[] foodDisplays;
    public Image[] beatIndicators;

    private List<int> currentSequence = new List<int>();
    private int currentInputIndex = 0;
    public TextMeshProUGUI feedbackText;
    public int sequencesCompleted = 0;
    private int sequencesToComplete = 10;
    public TextMeshProUGUI statusText;

    private bool isSequenceResetting = false;
    private bool isInTransition = false;
    private bool isInputProcessing = true;
    private bool isShowingRythm = false;
    
    private float beatPulseDuration = 0.3f;
    private float timingWindow = 0.4f;

    private List<float> expectedInputTiming = new List<float>();

    public AudioClip beatSound;
    private AudioSource audioSource;

    private bool gameStarted = false;
    public bool dialogueStartGame = false;
    private bool gameWon = false;

    [SerializeField] private PlayerInput playerInput;

    private List<List<float>> rhythmPresets = new List<List<float>>()
    {
        new List<float>{0.8f, 0.8f, 0.8f},
        new List<float>{0.5f, 1.0f, 0.5f},
        new List<float>{1.0f, 0.5f, 1.0f},
    };

    private List<float> currentRhythmPattern;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

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
        if (gameWon) return;

        isInTransition = false;
        isInputProcessing = false;
        isShowingRythm = true;

        GenerateNewSequence();
        StartSequencePlayback();
    }

    void GenerateNewSequence()
    {
        currentRhythmPattern = rhythmPresets[Random.Range(0, rhythmPresets.Count)];

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
    }

    void StartSequencePlayback()
    {
        isSequenceResetting = false;
        isInputProcessing = false;
        isShowingRythm = true;

        currentInputIndex = 0;

        foreach (var img in foodDisplays)
        {
            img.enabled = true;
        }

        StartCoroutine(ShowRhythm());
    }

    void HandleInput(int inputIndex)
    {
        if (!isInputProcessing || isShowingRythm || isInTransition) return;

        float inputTime = Time.time;

        if (currentInputIndex >= expectedInputTiming.Count)
        {
            Debug.LogWarning("No expected input time recorded for this index.");
            OnWrongInput();
            return;
        }

        float expectedTime = expectedInputTiming[currentInputIndex];
        float timeDifference = Mathf.Abs(inputTime - expectedTime);

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
        if (beatSound != null && audioSource != null)
        {
            audioSource.PlayOneShot(beatSound);
        }

        foodDisplays[currentInputIndex].enabled = false;
        currentInputIndex++;

        if (currentInputIndex >= currentSequence.Count)
        {
            isInputProcessing = false;
            isShowingRythm = false;
            isInTransition = true;

            feedbackText.text = "YAY";
            sequencesCompleted++;
            statusText.text = sequencesCompleted + " sequences completed!";

            Invoke("StartNewSequence", 1.0f);
        }
    }


    void OnWrongInput()
    {
        if (isSequenceResetting) return;
        isSequenceResetting = true;
        isInputProcessing = false;

        feedbackText.text = "Try again buddy";

        Invoke(nameof(StartSequencePlayback), 2.0f); // Retry the same sequence
    }

    private IEnumerator ShowRhythm()
    {
        feedbackText.text = "Watch carefully...";
        expectedInputTiming.Clear();

        float currentTime = Time.time;

        for (int i = 0; i < currentSequence.Count; i++)
        {
            expectedInputTiming.Add(currentTime);

            if (beatSound != null && audioSource != null)
            {
                audioSource.PlayOneShot(beatSound);
            }

            StartCoroutine(BeatPulse(i, 0f));

            float beatInterval = currentRhythmPattern[i];
            yield return new WaitForSeconds(beatInterval + 0.1f);

            currentTime += beatInterval + 0.1f;
        }

        feedbackText.text = "Now it's your turn!";
        isShowingRythm = false;
        isInputProcessing = true;

        SetExpectedInputTimes();
        StartCoroutine(BeatPulseInput());
    }

    void SetExpectedInputTimes()
    {
        expectedInputTiming.Clear();
        float currentTime = Time.time;

        for (int i = 0; i < currentRhythmPattern.Count; i++)
        {
            expectedInputTiming.Add(currentTime);
            currentTime += currentRhythmPattern[i];
        }
    }

    private IEnumerator BeatPulse(int index, float delay, Color? overrideColor = null)
    {
        yield return new WaitForSeconds(delay);

        Image pulse = beatIndicators[index];
        float duration = beatPulseDuration;
        float time = 0f;

        Color originalColor = pulse.color;
        Color pulseColor = overrideColor ?? originalColor;

        pulse.transform.localScale = Vector3.zero;
        pulse.color = new Color(pulseColor.r, pulseColor.g, pulseColor.b, 1f);

        while (time < duration)
        {
            time += Time.deltaTime;
            float scale = Mathf.Lerp(0f, 1f, time / duration);
            pulse.transform.localScale = new Vector3(scale, scale, scale);
            yield return null;
        }

        time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float alpha = Mathf.Lerp(1f, 0f, time / duration);
            pulse.color = new Color(pulseColor.r, pulseColor.g, pulseColor.b, alpha);
            yield return null;
        }

        pulse.transform.localScale = Vector3.zero;
        pulse.color = originalColor;
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
        gameWon = true;
    }

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