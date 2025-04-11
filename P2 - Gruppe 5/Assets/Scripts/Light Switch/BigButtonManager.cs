using UnityEngine;
using UnityEngine.UI;  // For UI elements like Image if needed
using DialogueEditor; // Assuming you have a DialogueEditor namespace for your dialogue system

public class YellowSpriteChecker : MonoBehaviour
{
    public SpriteRenderer[] targetSprites; // Assign 3 sprites in the Inspector
    public Color targetColor = Color.yellow; // The color to check for
    public Button winButton;
    private AudioSource audioSource;
    public AudioClip[] audioClips;   // Array of AudioClips (2 clips)
    private bool winConditionMet = false;
    [SerializeField] private NPCConversation myConversation;
    private void Start()
    {
        winButton.interactable = false;
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (CheckAllYellow() && !winConditionMet)
        {
            winConditionMet = true;
            PlayClip(0); // Play the first clip
            TriggerAction();
        }
    }

    bool CheckAllYellow()
    {
        foreach (SpriteRenderer sprite in targetSprites)
        {
            if (sprite.color != targetColor)
            {
                return false; // If any sprite is not yellow, return false
            }
        }
        return true; // All sprites are yellow
    }

    void TriggerAction()
    {
        Debug.Log("All sprites are yellow! Unnlocking end button");
        winButton.interactable = true;
    }

    public void OnWinButtonClick()
    {
        Debug.Log("YOU WIN THE ENTIRE GAME AND HAVE SET UP THE LIGHT SHOW!!!!");
        PlayClip(1); // Play the second clip
        winButton.interactable = false; // Disable the button after clicking
                                        // Start the conversation
        if (myConversation != null)
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
        else
        {
            Debug.LogWarning("No conversation assigned to the ConversationEditer.");
        }
    }
    void PlayClip(int index)
    {
        if (index >= 0 && index < audioClips.Length)
        {
            audioSource.clip = audioClips[index]; // Assign the new clip
            audioSource.Play(); // Play the selected clip
        }
    }
}