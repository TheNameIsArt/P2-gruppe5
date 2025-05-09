using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class PlatformWin : MonoBehaviour
{
    [SerializeField] NPCConversation conversation;
    [SerializeField] PlayerInput PlayerControllerCSH;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ConversationManager.Instance.StartConversation(conversation);
            PlayerControllerCSH.enabled = false; // Disable player movement
        }
    }
}
