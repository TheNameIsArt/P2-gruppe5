using UnityEngine;
using DialogueEditor;
public class InfoSceneDialogLoader : MonoBehaviour
{
    public NPCConversation DialogConvo;
    private static bool InfomationStart = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!InfomationStart)
        {
            InfomationStart = true;
            Debug.Log("This happens only the first time you enter the scene.");
            ConversationManager.Instance.StartConversation(DialogConvo);
        }
    }
}