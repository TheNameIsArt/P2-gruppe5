using DialogueEditor;
using UnityEngine;

public class SamFirstConvo : MonoBehaviour
{
    public NPCConversation myConversation;

    // Static variable to track if the conversation has played
    private static bool hasPlayed = false;

    void Start()
    {
        if (!hasPlayed && myConversation != null)
        {
            ConversationManager.Instance.StartConversation(myConversation);
            hasPlayed = true; // Set the flag so it won't play again
        }
        else if (myConversation == null)
        {
            Debug.LogWarning("No conversation assigned to the ConversationEditor.");
        }
    }
}
