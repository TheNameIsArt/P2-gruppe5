using UnityEngine;
using DialogueEditor;

public class BurgerManagerScript : MonoBehaviour
{
    // Four boolean flags
    public bool Thomas;
    public bool Simon;
    public bool Rusty;
    public bool SnC;
    public NPCConversation conversation;
    // Guard to ensure the method is only triggered once
    private bool hasTriggered = false;

    void Update()
    {
        if (!hasTriggered && Thomas && Simon && Rusty && SnC)
        {
            TriggerMethod();
            hasTriggered = true;
        }
    }

    // The method to trigger
    private void TriggerMethod()
    {
        // Your logic here
        Debug.Log("All four bools are true! Method triggered.");
        ConversationManager.Instance.StartConversation(conversation);
    }

    // Methods to toggle each bool
    public void ToggleThomas()
    {
        Thomas = !Thomas;
    }

    public void ToggleSimon()
    {
        Simon = !Simon;
    }

    public void ToggleRusty()
    {
        Rusty = !Rusty;
    }

    public void ToggleSnC()
    {
        SnC = !SnC;
    }
}
