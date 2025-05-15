using DialogueEditor;
using UnityEngine;

public class conversationStart : MonoBehaviour
{
    public NPCConversation conversation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }
}
