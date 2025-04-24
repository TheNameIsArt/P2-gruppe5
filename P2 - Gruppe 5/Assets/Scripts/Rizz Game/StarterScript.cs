using UnityEngine;
using UnityEngine.UI;
using DialogueEditor;
using UnityEngine.InputSystem;

public class StarterScript : MonoBehaviour
{
    public NPCConversation testConversation;

    public void OnButtonClick()
    {
        ConversationManager.Instance.StartConversation(testConversation);
    }

    void Update()
    {
        if (Gamepad.current != null && Gamepad.current.buttonWest.wasPressedThisFrame)
        {
            ConversationManager.Instance.StartConversation(testConversation);
        }
    }
}
