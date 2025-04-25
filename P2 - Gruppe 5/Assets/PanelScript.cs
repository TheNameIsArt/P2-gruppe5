using UnityEngine;
using UnityEngine.UI;
using System;

public class PanelScript : MonoBehaviour
{
    public Sprite[] panels; // Array of sprites to cycle through
    private SpriteRenderer spriteRenderer; // Reference to the SpriteRenderer
    private int currentIndex = 0; // Tracks the current sprite index
    public Button nextButton; // Reference to the button for next sprite
    public Button previousButton; // Reference to the button for previous sprite

    // Event to notify when the sprite index changes
    public event Action<int> OnSpriteIndexChanged;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        
        // Ensure the SpriteRenderer starts with the first sprite
        if (panels.Length > 0 && spriteRenderer != null)
        {
            spriteRenderer.sprite = panels[currentIndex];
        }

        // Add listeners to the buttons
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(NextPanel);
        }

        if (previousButton != null)
        {
            previousButton.onClick.AddListener(PreviousPanel);
        }
    }

    private void NextPanel()
    {
        if (panels.Length == 0) return;

        // Increment index and loop back if necessary
        currentIndex = (currentIndex + 1) % panels.Length;
        UpdateSprite();
    }

    private void PreviousPanel()
    {
        if (panels.Length == 0) return;

        // Decrement index and loop back if necessary
        currentIndex = (currentIndex - 1 + panels.Length) % panels.Length;
        UpdateSprite();
    }

    private void UpdateSprite()
    {
        if (spriteRenderer != null)
        {
            spriteRenderer.sprite = panels[currentIndex];
        }

        // Trigger the event to notify listeners of the index change
        OnSpriteIndexChanged?.Invoke(currentIndex);
    }

    public int GetCurrentIndex()
    {
        return currentIndex;
    }
}
