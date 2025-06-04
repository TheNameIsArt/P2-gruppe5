using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MapButtonManager : MonoBehaviour
{
    [SerializeField] private Button[] mapButtons; // Array to hold all map buttons
    private Button currentlySelectedButton; // Tracks the currently selected button
    public string correctArea; // Correct area (for example, "Area A", "Area B", etc.)
    [SerializeField] private Button confirmButton;
    [SerializeField] private GameObject[] bots; // Array to hold bot GameObjects
    private int currentBotIndex = 0; // Tracks the current bot to trigger
    private bool botIsExiting = false; // Flag to check if the bot is exiting
    [SerializeField] private TimeManager timeManager; // Reference to the TimeManager script

    // This method is called when a button is clicked
    public void OnMapButtonClicked(Button clickedButton)
    {
        // Re-enable the previously selected button if it exists
        if (currentlySelectedButton != null)
        {
            currentlySelectedButton.interactable = true;
        }

        // Disable the interactability of the clicked button
        clickedButton.interactable = false;

        // Update the currently selected button
        currentlySelectedButton = clickedButton;

        // Set the clicked button as the currently selected GameObject in the EventSystem
        EventSystem.current.SetSelectedGameObject(clickedButton.gameObject);
    }

    // Method to check if the selected button matches the correct area
    public void OnConfirmButtonClicked()
    {
        if (currentlySelectedButton == null)
        {
            Debug.LogWarning("No button is currently selected!");
            return;
        }

        // Check if the name of the currently selected button matches the correctArea
        if (currentlySelectedButton.name == correctArea && !botIsExiting)
        {
            CorrectMatch();
        }
        else if (!botIsExiting)
        {
            IncorrectMatch();
        }
    }

    // Logic for correct match
    private void CorrectMatch()
    {
        Debug.Log("Correct match logic executed.");
        botIsExiting = true; // Set the flag to true to prevent further interactions
        // Trigger the CorrectZone method on the current bot
        if (currentBotIndex < bots.Length && bots[currentBotIndex] != null)
        {
            campBotScript botScript = bots[currentBotIndex].GetComponent<campBotScript>();
            if (botScript != null)
            {
                botScript.CorrectZone();
            }

            // Start a coroutine to enable the next bot after a delay
            StartCoroutine(EnableNextBotAfterDelay());
        }
    }

    // Coroutine to enable the next bot after a delay
    private System.Collections.IEnumerator EnableNextBotAfterDelay()
    {
        // Wait for 2 seconds
        yield return new WaitForSeconds(2f);

        // Enable the next bot in the array, if it exists
        currentBotIndex++;
        if (currentBotIndex < bots.Length && bots[currentBotIndex] != null)
        {
            bots[currentBotIndex].SetActive(true);
            botIsExiting = false; // Reset the flag to allow further interactions
        }
    }

    // Logic for incorrect match
    private void IncorrectMatch()
    {
        Debug.Log("Incorrect match logic executed.");
        timeManager.AddTenPercentToElapsedTime(); // Add 10% of the total time to the elapsed time
    }

    // Optional: Initialize the buttons with listeners
    private void Start()
    {
        foreach (Button button in mapButtons)
        {
            button.onClick.AddListener(() => OnMapButtonClicked(button));
        }

        // Add a listener to the confirmButton
        confirmButton.onClick.AddListener(OnConfirmButtonClicked);

        // Ensure only the first bot is active at the start
        /*for (int i = 0; i < bots.Length; i++)
        {
            bots[i].SetActive(i == 0);
        }*/
    }
    public void ActivateFirstBot()
    {
        // Reset the current bot index to 0
        currentBotIndex = 0;

        // Deactivate all bots first
        foreach (GameObject bot in bots)
        {
            if (bot != null)
            {
                bot.SetActive(false);
            }
        }

        // Activate the first bot if it exists
        if (bots.Length > 0 && bots[0] != null)
        {
            bots[0].SetActive(true);
            Debug.Log("First bot activated.");
        }
        else
        {
            Debug.LogWarning("No bots available to activate.");
        }
    }
}
