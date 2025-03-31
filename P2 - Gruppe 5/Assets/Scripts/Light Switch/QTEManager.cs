using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class QTEManager : MonoBehaviour
{
    public Text[] qteTexts; // UI Text elements to display 5 keys
    public Image timerBar; // UI Timer bar
    public float timeLimit = 2f;

    private QTEInputActions inputActions;
    private List<KeyCode> currentKeys = new List<KeyCode>(); // Stores the 5 active keys
    private bool qteActive = false;
    private float timer;

    public UnityEvent onQTESuccess;
    public UnityEvent onQTEFail;

    private KeyCode[] possibleKeys = { KeyCode.A, KeyCode.S, KeyCode.D, KeyCode.W, KeyCode.Space };

    void Awake()
    {
        inputActions = new QTEInputActions();
    }

    void OnEnable()
    {
        inputActions.Enable();
        StartQTE();
    }

    void OnDisable()
    {
        inputActions.Disable();
    }

    void Update()
    {
        if (qteActive)
        {
            timer -= Time.deltaTime;
            if (timerBar) timerBar.fillAmount = timer / timeLimit;

            if (timer <= 0)
            {
                QTEFail();
            }
        }
    }

    void StartQTE()
    {
        currentKeys.Clear();

        // Pick 5 unique random keys
        List<KeyCode> keyPool = new List<KeyCode>(possibleKeys);
        for (int i = 0; i < qteTexts.Length; i++)
        {
            if (keyPool.Count == 0) break; // Safety check

            int index = Random.Range(0, keyPool.Count);
            KeyCode selectedKey = keyPool[index];
            keyPool.RemoveAt(index); // Remove to prevent duplicates

            currentKeys.Add(selectedKey);
            qteTexts[i].text = selectedKey.ToString();
        }

        timer = timeLimit;
        qteActive = true;
    }

    void OnGUI()
    {
        if (!qteActive) return;

        Event e = Event.current;
        if (e.isKey && e.type == EventType.KeyDown)
        {
            if (currentKeys.Contains(e.keyCode))
            {
                QTESuccess();
            }
            else
            {
                QTEFail();
            }
        }
    }

    void QTESuccess()
    {
        Debug.Log("QTE Success!");
        foreach (var text in qteTexts) text.text = "Success!";
        qteActive = false;
        onQTESuccess?.Invoke();
        Invoke(nameof(StartQTE), 1f);
    }

    void QTEFail()
    {
        Debug.Log("QTE Failed!");
        foreach (var text in qteTexts) text.text = "Failed!";
        qteActive = false;
        onQTEFail?.Invoke();
        Invoke(nameof(StartQTE), 1f);
    }
}
