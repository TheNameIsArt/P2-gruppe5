using UnityEngine;
using DialogueEditor;

public class ConversationChanger : MonoBehaviour
{
    [SerializeField] private DialogueEventTrigger dialogueEventTrigger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (CampingSpawner.Instance.talkedWithCampBots)
        {
            dialogueEventTrigger.ChangeConversation(1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
