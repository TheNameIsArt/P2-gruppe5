using System.Collections;
using UnityEngine;
using DialogueEditor;

/// <summary>
/// Handles loading and switching dialogue conversations for a character,
/// and registers the current conversation state with the DialogueManager.
/// </summary>
public class DialogueEventTrigger : MonoBehaviour
{
    // Unique identifier for this character (can be set in Inspector or defaults to GameObject name)
    public string characterID;

    // Reference to the ConversationEditer component that manages the current conversation
    public ConversationEditer npcDialogue;

    // Array of all possible conversations for this character (assign in Inspector)
    public NPCConversation[] possibleConversations;

    /// <summary>
    /// On start, attempts to load the saved conversation for this character.
    /// If none is saved, registers the current conversation as the initial state.
    /// </summary>
    private IEnumerator Start()
    {
        yield return null; // Wait one frame to ensure all managers are initialized

        // If no characterID is set, use the GameObject's name
        if (string.IsNullOrEmpty(characterID))
        {
            characterID = gameObject.name;
        }

        // If the DialogueManager exists, try to load or register the conversation
        if (DialogueManager.Instance != null)
        {
            // Try to get a previously saved conversation for this character
            if (DialogueManager.Instance.TryGetSavedConversation(characterID, possibleConversations, out var savedConversation))
            {
                npcDialogue.myConversation = savedConversation;
                Debug.Log($"[DialogueEventTrigger] {characterID} loaded saved conversation: {savedConversation.name}");
            }
            else
            {
                // No saved conversation found, register the current one as the initial state
                DialogueManager.Instance.RegisterDialogueState(characterID, npcDialogue.myConversation);
                Debug.Log($"[DialogueEventTrigger] {characterID} registered initial conversation: {npcDialogue.myConversation.name}");
            }
        }
    }

    /// <summary>
    /// Switches to a different conversation by index and updates the DialogueManager.
    /// </summary>
    /// <param name="conversationIndex">Index of the conversation in possibleConversations array.</param>
    public void ChangeConversation(int conversationIndex)
    {
        // Ensure the index is valid and the npcDialogue reference exists
        if (npcDialogue != null && conversationIndex >= 0 && conversationIndex < possibleConversations.Length)
        {
            var selected = possibleConversations[conversationIndex];
            npcDialogue.myConversation = selected;
            DialogueManager.Instance.RegisterDialogueState(characterID, selected);
            Debug.Log($"[DialogueEventTrigger] {characterID} switched to conversation: {selected.name}");
        }
    }
}
