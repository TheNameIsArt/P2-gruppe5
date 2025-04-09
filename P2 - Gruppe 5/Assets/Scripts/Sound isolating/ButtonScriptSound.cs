using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class ButtonScriptSound : MonoBehaviour
{
    [SerializeField]
    private GameObject ui;
    public SineManipulator SineManipulator;
    private bool buttonPressed = false;
    public Slider progressBar;

    // List of buttons to disable while the button is pressed
    private List<Button> allButtons = new List<Button>();

    private void Start()
    {
        // Find all GameObjects with the "SoundWaves" tag
        GameObject[] soundWaveObjects = GameObject.FindGameObjectsWithTag("SoundWaves");

        // Get the Button component from each GameObject and add it to the list
        foreach (GameObject obj in soundWaveObjects)
        {
            Button button = obj.GetComponent<Button>();
            if (button != null)
            {
                allButtons.Add(button);
            }
        }
    }

    public void OnButtonClick()
    {
        if (ui != null)
        {
            Debug.Log("Button Clicked!");
            ui.SetActive(true);

            // Find SineManipulator and set buttonPressed to true
            SineManipulator = GameObject.Find("PlayerControlled Sinewave").GetComponent<SineManipulator>();
            if (SineManipulator != null)
            {
                SineManipulator.Won = false;
                buttonPressed = true;
                Cursor.lockState = CursorLockMode.Locked;

                // Assign this GameObject's AudioSource to the SineManipulator's currentAudio
                AudioSource audioSource = GetComponent<AudioSource>();
                if (audioSource != null)
                {
                    SineManipulator.currentAudio = audioSource;
                    Debug.Log("AudioSource assigned to SineManipulator.");
                }
                else
                {
                    Debug.LogWarning("No AudioSource found on this GameObject.");
                }

                // Disable all buttons while the action is in progress
                DisableAllButtons();
            }
            else
            {
                Debug.Log("SineManipulator not found!");
            }
        }
    }

    private void LateUpdate()
    {
        if (buttonPressed && SineManipulator.Won)
        {
            this.gameObject.SetActive(false);
            EnableAllButtons();  // Re-enable all buttons when the game is won
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void DisableAllButtons()
    {
        // Disable each button in the list to prevent interaction
        foreach (Button btn in allButtons)
        {
            btn.interactable = false;
        }
    }

    private void EnableAllButtons()
    {
        // Re-enable each button in the list when needed
        foreach (Button btn in allButtons)
        {
            btn.interactable = true;
        }
    }
}
