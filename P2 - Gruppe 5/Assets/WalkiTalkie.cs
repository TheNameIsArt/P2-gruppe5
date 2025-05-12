using UnityEngine;
using DialogueEditor;
public class WalkiTalkie : MonoBehaviour
{
    public NPCConversation WalkiTalkiConvo;
    private static bool delilahHasEnteredScene = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!delilahHasEnteredScene)
        {
            delilahHasEnteredScene = true;
            Debug.Log("This happens only the first time you enter the scene.");
            ConversationManager.Instance.StartConversation(WalkiTalkiConvo);
        }
    }
}
