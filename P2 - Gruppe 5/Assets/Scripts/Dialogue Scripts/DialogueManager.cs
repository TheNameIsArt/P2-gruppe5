using DialogueEditor;
using System.Collections.Generic;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    private Dictionary<string, string> savedConversationNames = new Dictionary<string, string>();

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public bool TryGetSavedConversation(string characterID, NPCConversation[] possibleConversations, out NPCConversation conversation)
    {
        if (savedConversationNames.TryGetValue(characterID, out var savedName))
        {
            conversation = System.Array.Find(possibleConversations, c => c.name == savedName);
            return conversation != null;
        }

        conversation = null;
        return false;
    }

    public void RegisterDialogueState(string characterID, NPCConversation conversation)
    {
        if (conversation != null)
        {
            savedConversationNames[characterID] = conversation.name;
        }
    }
    public void SetConversationByIndex(string characterID, NPCConversation[] possibleConversations, int index)
    {
        if (possibleConversations == null || index < 0 || index >= possibleConversations.Length)
            return;

        var conversation = possibleConversations[index];
        if (conversation != null)
        {
            savedConversationNames[characterID] = conversation.name;
        }
    }
}