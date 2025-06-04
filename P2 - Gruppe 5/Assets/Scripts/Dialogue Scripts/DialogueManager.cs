using DialogueEditor;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Manages saving and retrieving the current conversation state for each character.
/// Implements a singleton pattern and persists across scenes.
/// </summary>
public class DialogueManager : MonoBehaviour
{
    // Singleton instance
    public static DialogueManager Instance { get; private set; }

    // Stores the saved conversation name for each characterID
    private Dictionary<string, string> savedConversationNames = new Dictionary<string, string>();

    // Ensure only one instance exists and persists between scenes
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Destroy duplicate
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Persist across scene loads
        }
    }

    /// <summary>
    /// Attempts to retrieve the saved conversation for a character.
    /// </summary>
    /// <param name="characterID">The unique ID for the character.</param>
    /// <param name="possibleConversations">Array of possible conversations for this character.</param>
    /// <param name="conversation">The found conversation, or null if not found.</param>
    /// <returns>True if a saved conversation was found and matched; otherwise, false.</returns>
    public bool TryGetSavedConversation(string characterID, NPCConversation[] possibleConversations, out NPCConversation conversation)
    {
        if (savedConversationNames.TryGetValue(characterID, out var savedName))
        {
            // Find the conversation in the array that matches the saved name
            conversation = System.Array.Find(possibleConversations, c => c.name == savedName);
            return conversation != null;
        }

        conversation = null;
        return false;
    }

    /// <summary>
    /// Registers (saves) the current conversation for a character.
    /// </summary>
    /// <param name="characterID">The unique ID for the character.</param>
    /// <param name="conversation">The conversation to save.</param>
    public void RegisterDialogueState(string characterID, NPCConversation conversation)
    {
        if (conversation != null)
        {
            savedConversationNames[characterID] = conversation.name;
        }
    }
}
