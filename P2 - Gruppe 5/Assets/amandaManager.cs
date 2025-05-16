using DialogueEditor;
using Unity.VisualScripting;
using UnityEngine;

public class amandaManager : MonoBehaviour
{
    public static bool amandaHappy = false;
    private static bool amandaConvo = false; // Flag to check if amandaHappy has been set
    public GameObject amanda;
    public GameObject amandaHappyObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (amandaHappy)
        {
            Debug.Log("Amanda is happy!");
            Destroy(amanda);
            amandaHappyObject.SetActive(true);
        }
        else
        {
            Debug.Log("Amanda is not happy.");
            amanda.SetActive(true);
            amandaHappyObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public void MakeAmandaHappy()
    {
        amandaHappy = true;
        Destroy(amanda);
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
