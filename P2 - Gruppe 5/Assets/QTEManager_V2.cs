using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class QTEManager_V2 : MonoBehaviour
{
    public TMP_Text qteText; // Assign a TextMeshPro UI text in the inspector
    public float timeLimit = 5f; // Time to complete each sequence
    public int sequenceLength = 3; // Number of inputs per sequence
    public int totalSequences = 5; // Total sequences before success
    public InputActionAsset inputActions; // Assign Input Actions Asset in Inspector
    public Button startButton; // Assign a UI Button in the inspector
    public SpriteRenderer winLight; // Assign the circle sprite in the Inspector

    private List<string> qteSequence = new List<string>();
    private int currentIndex = 0;
    private float timer;
    private bool qteActive = false;
    private int sequencesCompleted = 0; // Track completed sequences
    private InputActionMap actionMap;
    private Dictionary<string, InputAction> inputActionsMap = new Dictionary<string, InputAction>();

    private string[] possibleInputs = { "Left", "Down", "Right", "Up" }; // Mapped to input actions

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
        sequencesCompleted = 0; // Reset completed sequences
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
        if (timer <= 0)
        {
            LoseQTE();
        }
    }

    void OnInputReceived(string input)
    {
        if (!qteActive) return;

        if (input == qteSequence[currentIndex])
        {
            currentIndex++;
            if (currentIndex >= qteSequence.Count)
            {
                sequencesCompleted++;
                qteActive = false;
                StartNextSequence();
            }
        }
        else
        {
            LoseQTE();
        }
    }

    void WinQTE()
    {
        qteText.text = "<color=green>Success! All sequences completed!</color>";
        qteActive = false;

        // Change the color from black to yellow
        if (winLight != null)
        {
            winLight.color = Color.yellow;
        }
    }

    void LoseQTE()
    {
        qteText.text = "<color=red>Failed! Try again.</color>";
        qteActive = false;
    }
}
