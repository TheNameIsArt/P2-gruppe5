using DialogueEditor;
using UnityEngine;

public class CrowdByeBye : MonoBehaviour
{
    private static bool burgerGameStarted = false;
    private static bool burgerGameFinished = false;
    private static bool convoplayed = false;
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
                if (!convoplayed)
                {
                    ConversationManager.Instance.StartConversation(conversation);
                    convoplayed = true;
                }
                
            }
            gameObject.SetActive(false);

        }
    }
    public void StartBurgerGame()
    {
        burgerGameStarted = true;
        
    }
}
