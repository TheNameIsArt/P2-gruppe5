using DialogueEditor;
using UnityEngine;

public class ConversationEditer : MonoBehaviour
{
    [SerializeField] private NPCConversation currentConversation;
    [SerializeField] private NPCConversation[] conversations;
    [SerializeField] private int currentConversationIndex = 0;
    [SerializeField] private bool isConversationActive = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        conversations = GetComponentsInChildren<NPCConversation>();
        if (conversations.Length > 0)
        {
            currentConversation = conversations[0]; // Set the first conversation as the current one
            isConversationActive=ConversationManager.Instance.IsConversationActive;
            

        }
    }

    public void SwitchToNextConversation()
    {
        if (conversations.Length > 0)
        {
            currentConversationIndex = (currentConversationIndex + 1) % conversations.Length;
            currentConversation = conversations[currentConversationIndex];
        }
    }

    public void PlayConversation()
    {
        if (currentConversation != null)
        {
            ConversationManager.Instance.StartConversation(currentConversation);
        }
    }
}
