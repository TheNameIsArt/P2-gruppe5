using DialogueEditor;
using Unity.VisualScripting;
using UnityEngine;

public class amandaManager : MonoBehaviour
{
    public static bool amandaHappy = false;
    private static bool amandaConvo = false; // Flag to check if amandaHappy has been set
    public GameObject amanda;
    public GameObject amandaHappyObject;
    public bool amandaActive = false; // Flag to check if Amanda is active in the scene
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        if (HatManager.Instance.sabrinaQuestStarter)
        {
            amanda.SetActive(true); // Ensure Amanda is active in the scene
            amandaHappyObject.SetActive(false); // Hide the happy object initially
        }
        else
        {
            amanda.SetActive(false); // Hide Amanda if not active
            amandaHappyObject.SetActive(false); // Ensure happy object is also hidden
        }
    }

    // Update is called once per frame
    public void MakeAmandaHappy()
    {
        amandaHappy = true;
        amanda.SetActive(false); // Hide the amanda GameObject
        amandaHappyObject.SetActive(true);
        amandaConvo = true; // Set the flag to true when amandaHappy is set
    }

    private void Update()
    {
        if (amandaConvo && !ConversationManager.Instance.IsConversationActive)
        {
            amandaConvo = false; // Reset the flag after the conversation is done
            //HatManager.Instance.AmandaTalkComplete = true;
        }
    }
    public void hatDestroyer()
    {
     HatManager.Instance.DestroyThis();
    }
}
