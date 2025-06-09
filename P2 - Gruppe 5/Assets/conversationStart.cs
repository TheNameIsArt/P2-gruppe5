using DialogueEditor;
using UnityEngine;

public class conversationStart : MonoBehaviour
{
    public NPCConversation conversation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (conversation == null)
        {
            gameObject.SetActive(false);
            return;
        }
        ConversationManager.Instance.StartConversation(conversation);
    }

    public void OnConversationStop()
    {
        conversation = null; // Clear the conversation reference
    }
}
