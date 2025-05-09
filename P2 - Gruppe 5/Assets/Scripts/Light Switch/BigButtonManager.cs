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
    public bool winConditionMet = false;
    [SerializeField] private NPCConversation myConversation;
    [SerializeField] private GameObject virtualMouse; // Reference to the virtual mouse GameObject
    public bool light1;
    public bool light2;
    public bool light3;
    [SerializeField] private Animator winAnimator; // Reference to the animation component
    [SerializeField] private AnimationClip winAnimation; // Reference to the animation clip
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
        // Check if all three boolean variables are true
        return light1 && light2 && light3;
    }

    void TriggerAction()
    {
        Debug.Log("All sprites are yellow! Unnlocking end button");
        winButton.interactable = true;
        winAnimator.Play(winAnimation.name); // Play the animation
    }

    public void OnWinButtonClick()
    {
        Debug.Log("YOU WIN THE ENTIRE GAME AND HAVE SET UP THE LIGHT SHOW!!!!");
        PlayClip(1); // Play the second clip
        winButton.interactable = false; // Disable the button after clicking
                                        // Start the conversation
        if (myConversation != null)
        {
            virtualMouse.SetActive(false); // Disable the virtual mouse
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