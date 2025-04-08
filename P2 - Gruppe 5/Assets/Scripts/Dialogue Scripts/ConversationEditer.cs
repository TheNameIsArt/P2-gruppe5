using DialogueEditor;
using UnityEngine;

public class ConversationEditer : MonoBehaviour
{
    public NPCConversation myConversation;


    public void PlayConversation()
    {
        if (myConversation != null)
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
        else
        {
            Debug.LogWarning("No conversation assigned to the ConversationEditer.");
        }
    }
}
