using DialogueEditor;
using UnityEngine;

public class ConversationEditer : MonoBehaviour
{
    public NPCConversation currentConversation;
    public NPCConversation[] conversations;
    private NPCConversation targetConversation;
    private int currentConversationIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conversations = GetComponentsInChildren<NPCConversation>();
        if (conversations.Length > 0)
        {
            currentConversation = conversations[0]; // Set the first conversation as the current one
        }
    }

    public void SetTargetConversation(NPCConversation conversation)
    {
        targetConversation = conversation;
        if (targetConversation == currentConversation)
        {
            SwitchToNextConversation();
        }
    }

    private void SwitchToNextConversation()
    {
        if (conversations.Length > 0)
        {
            currentConversationIndex = (currentConversationIndex + 1) % conversations.Length;
            currentConversation = conversations[currentConversationIndex];
        }
    }
}
