using DialogueEditor;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction_Controller : MonoBehaviour
{
    public string targetSceneName;
    public NPCConversation npcConversation;

    void Start()
    {
        npcConversation = GetComponent<NPCConversation>();
    }
    
    void Update()
    {
        
    }
}
