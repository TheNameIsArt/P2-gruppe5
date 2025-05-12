using UnityEngine;
using DialogueEditor;
using System.Collections;

public class WalkiTalkie : MonoBehaviour
{
    public NPCConversation WalkiTalkiConvo;
    private static bool hasEnteredScene = false;
    [SerializeField] private GameObject convoZone; // Reference to the cat prefab

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!hasEnteredScene)
        {
            hasEnteredScene = true;
            Debug.Log("This happens only the first time you enter the scene.");
            ConversationManager.Instance.StartConversation(WalkiTalkiConvo);
            convoZone.SetActive(false); // Deactivate convo zone at the start
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
