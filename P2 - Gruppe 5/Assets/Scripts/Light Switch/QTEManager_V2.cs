using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QTEManager_V2 : MonoBehaviour
{
    public TMP_Text qteText;
    public float timeLimit = 5f;
    public int sequenceLength = 5;
    public int totalSequences = 5;
    public InputActionAsset inputActions;
    public Button startButton;
    public SpriteRenderer winLight;
    public AudioSource audioSource;
    public AudioClip[] audioClips;
    public SpriteRenderer[] sequenceLights;
    public Slider timerSlider;

    private List<string> qteSequence = new List<string>();
    private int currentIndex = 0;
    private float timer;
    private bool qteActive = false;
    private int sequencesCompleted = 0;
    private InputActionMap actionMap;
    private Dictionary<string, InputAction> inputActionsMap = new Dictionary<string, InputAction>();

    public SpriteRenderer[] inputSprites; // Assign 3 SpriteRenderers in Inspector

    [Header("Input Sprites Mapping")]
    public string[] inputNames; // Names: "Left", "Down", "Right", "Up"
    public Sprite[] inputIcons; // Sprites corresponding to input names

    private string[] possibleInputs = { "Left", "Down", "Right", "Up" };

    void Awake()
    {
        actionMap = inputActions.FindActionMap("QTE");
        foreach (var input in possibleInputs)
        {
            InputAction action = actionMap.FindAction(input);
            if (action != null)
            {
                action.performed += ctx => OnInputReceived(input);
                inputActionsMap[input] = action;
            }
        }
        audioSource = GetComponent<AudioSource>();
    }

    void OnEnable()
    {
        actionMap.Enable();
    }

    void OnDisable()
    {
        actionMap.Disable();
    }

    void Start()
    {
        startButton.onClick.AddListener(StartQTE);
    }

    void StartQTE()
    {
        sequencesCompleted = 0;
        startButton.interactable = false;
        StartNextSequence();
    }

    void StartNextSequence()
    {
        if (sequencesCompleted >= totalSequences)
        {
            WinQTE();
            return;
        }

        qteSequence.Clear();
        for (int i = 0; i < sequenceLength; i++)
        {
            qteSequence.Add(possibleInputs[Random.Range(0, possibleInputs.Length)]);
        }

        DisplaySequence();
        timer = timeLimit;
        currentIndex = 0;
        qteActive = true;
        ResetInputSprites(); // Reset all sprite colors
        UpdateInputSprites();
    }

    void DisplaySequence()
    {
        qteText.text = $"Sequence {sequencesCompleted + 1}/{totalSequences}\nPress: ";
        foreach (string input in qteSequence)
        {
            qteText.text += input + " ";
        }
    }

    void Update()
    {
        if (!qteActive) return;

        timer -= Time.deltaTime;
        timerSlider.value = timer / timeLimit;
        if (timer <= 0)
        {
            LoseQTE();
        }
    }

    void OnInputReceived(string input)
    {
        if (!qteActive) return;
        PlayClip(0);

        if (input == qteSequence[currentIndex])
        {
            // Correct input: turn green
            if (currentIndex < inputSprites.Length)
            {
                inputSprites[currentIndex].color = Color.green;
            }

            currentIndex++;

            if (currentIndex >= qteSequence.Count)
            {
                sequencesCompleted++;
                qteActive = false;
                sequenceLights[sequencesCompleted - 1].color = Color.white;
                StartNextSequence();
            }
        }
        else
        {
            // Incorrect input: turn red
            if (currentIndex < inputSprites.Length)
            {
                inputSprites[currentIndex].color = Color.red;
            }
            LoseQTE();
        }
    }
    void ResetInputSprites()
    {
        foreach (var sprite in inputSprites)
        {
            sprite.color = Color.white; // Reset to original color
        }
    }

    void UpdateInputSprites()
    {
        for (int i = 0; i < inputSprites.Length; i++)
        {
            int sequenceIndex = currentIndex + i; // Track input in sequence

            if (sequenceIndex < qteSequence.Count)
            {
                string nextInput = qteSequence[sequenceIndex];
                Sprite inputSprite = GetSpriteForInput(nextInput);

                if (inputSprite != null)
                {
                    inputSprites[i].sprite = inputSprite;
                    inputSprites[i].enabled = true;

                    // Keep already tapped inputs green
                    inputSprites[i].color = (sequenceIndex < currentIndex) ? Color.green : Color.white;
                }
            }
        }
    }

    Sprite GetSpriteForInput(string input)
    {
        for (int i = 0; i < inputNames.Length; i++)
        {
            if (inputNames[i] == input)
            {
                return inputIcons[i];
            }
        }
        return null;
    }

    void WinQTE()
    {
        qteText.text = "<color=green>Success! All sequences completed!</color>";
        qteActive = false;
        PlayClip(1);
        if (winLight != null)
        {
            winLight.color = new Color(1f, 1f, 1f, 1f);
        }
    }

    void LoseQTE()
    {
        qteText.text = "<color=red>Failed! Try again.</color>";
        qteActive = false;
        startButton.interactable = true;
        PlayClip(2);
    }

    void PlayClip(int index)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            audioSource.clip = audioClips[index];
            audioSource.Play();
        }
    }
}
