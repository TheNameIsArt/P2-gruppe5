using DialogueEditor;
using UnityEngine;

public class CrowdByeBye : MonoBehaviour
{
    private static bool burgerGameStarted = false;
    private static bool burgerGameFinished = false;
    public NPCConversation conversation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (burgerGameStarted)
        {
            if (!burgerGameFinished)
            {
                burgerGameStarted = true;
                Debug.Log("This happens only the first time you enter the scene.");
                ConversationManager.Instance.StartConversation(conversation);
            }
            gameObject.SetActive(false);

        }
    }
    public void StartBurgerGame()
    {
        burgerGameStarted = true;
        
    }
}
