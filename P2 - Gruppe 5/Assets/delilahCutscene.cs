using DialogueEditor;
using UnityEngine;

public class delilahCutscene : MonoBehaviour
{
    [SerializeField] private NPCConversation conversation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ConversationManager.Instance.StartConversation(conversation);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
