using UnityEngine;
using DialogueEditor;

public class StupidNPCdialog : MonoBehaviour
{

    public NPCConversation myConversation;

    private void OnMouseOver()
    {
        if(Input.GetMouseButtonDown(0))
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
    }   
}
