using UnityEngine;
using DialogueEditor;

public class DelilahConversation : MonoBehaviour
{
    [SerializeField] private string correctHat;
    [SerializeField] private ConversationEditer conversationEditor; // assuming this is needed
    [SerializeField] private Scr_HatSwitcher hatSwitcher;
    [SerializeField] NPCConversation conversation;

    private void Start()
    {
        conversationEditor = GetComponent<ConversationEditer>();
    }
    public void PlayConversation()
    {
        if(hatSwitcher.currentHat == correctHat)
        {
            conversationEditor.PlayConversation();
        }
        else
        {
            ConversationManager.Instance.StartConversation(conversation);
        }
    }
}
