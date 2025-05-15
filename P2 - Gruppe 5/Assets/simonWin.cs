using DialogueEditor;
using UnityEngine;

public class simonWin : MonoBehaviour
{
    private static bool simonWon = false;
    
    public NPCConversation conversation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (simonWon)
        {
            simonWon = false;
            ConversationManager.Instance.StartConversation(conversation);

        }
    }
    public void StartBurgerGame()
    {
        simonWon = true;

    }
}
