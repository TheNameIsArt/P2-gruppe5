using UnityEngine;
using DialogueEditor;

public class buck : MonoBehaviour
{
    public NPCConversation conversation;
    public static bool isConversationStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!isConversationStarted)
        {
            isConversationStarted = true;
            ConversationManager.Instance.StartConversation(conversation);
        }
        
    }
}
