using UnityEngine;
using DialogueEditor;
using System.Reflection;

public class HatManager : MonoBehaviour
{
    public static HatManager Instance { get; private set; }

    public string SavedHat = "NoHat"; // Default value

    public  bool burgerComplete = false;
    public  bool constructionComplete = false;
    public  bool lightSwitchComplete = false;
    public  bool sabrinaQuestStarter = false;
    public bool BitTalkComplete = false;
    public bool FrankTalkComplete = false;
    public bool AmandaTalkComplete = false;
    public bool sabrinaQuestComplete = false;
    public NPCConversation sabrinaQuestConvo;
    public NPCConversation FinalTalkConvo;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (burgerComplete && constructionComplete && lightSwitchComplete && !sabrinaQuestStarter)
        {
            sabrinaQuestStarter = true;
            if (sabrinaQuestConvo != null)
            {
                ConversationManager.Instance.StartConversation(sabrinaQuestConvo);
            }
        }

        if (BitTalkComplete && FrankTalkComplete && AmandaTalkComplete && !sabrinaQuestComplete)
        {
            sabrinaQuestComplete = true;
            Debug.Log("All talk completions are true.");
            ConversationManager.Instance.StartConversation(FinalTalkConvo);
        }
        if (sabrinaQuestComplete)
        {
            Debug.Log("Sabrina quest is complete.");

            // Perform any additional actions when the quest is complete
        }
    }
    public void TurnBoolOn(string boolName)
    {
        var field = GetType().GetField(boolName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
        if (field != null && field.FieldType == typeof(bool))
        {
            field.SetValue(this, true);
            Debug.Log($"[HatManager] Bool '{boolName}' set to true.");
        }
        else
        {
            Debug.LogWarning($"[HatManager] Bool field '{boolName}' not found.");
        }
    }
}
