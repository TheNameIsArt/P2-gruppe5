using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;

public class StarterScript : MonoBehaviour
{
    public NPCConversation testConversation;
public void OnButtonClick()
    {
        ConversationManager.Instance.StartConversation(testConversation);
    }
}
