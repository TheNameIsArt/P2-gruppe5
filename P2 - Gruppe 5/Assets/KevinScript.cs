using DialogueEditor;
using UnityEngine;

public class KevinScript : MonoBehaviour
{
    private static bool KevinWon = false;

    public NPCConversation conversation;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (KevinWon)
        {
            KevinWon = false;
            ConversationManager.Instance.StartConversation(conversation);

        }
    }
    public void StartBurgerGame()
    {
        KevinWon = true;

    }
}
