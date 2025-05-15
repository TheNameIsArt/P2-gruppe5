using UnityEngine;
using DialogueEditor;

public abstract class DialogSceneLoader : MonoBehaviour
{
    public NPCConversation DialogConvo;
    protected static bool InfomationStart = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        if (!InfomationStart)
        {
            InfomationStart = true; // Set to true only the first time Start runs
            gameObject.SetActive(false);
        }
    }

    public virtual void StartGame()
    {
        InfomationStart = true;
    }
}