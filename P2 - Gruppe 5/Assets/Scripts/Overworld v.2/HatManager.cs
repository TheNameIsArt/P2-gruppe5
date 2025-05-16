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
    public Level_Changer levelChanger;

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
            
            Debug.Log("All talk completions are true.");
            levelChanger = GameObject.FindGameObjectWithTag("DelilahCutscene").GetComponent<Level_Changer>();
            ConversationManager.Instance.StartConversation(FinalTalkConvo);
            sabrinaQuestComplete = true;
        }
        if (sabrinaQuestComplete && !ConversationManager.Instance.IsConversationActive)
        {
            sabrinaQuestComplete = false; // Reset the quest completion status
            Debug.Log("Sabrina quest is complete.");
            levelChanger.sceneChanger();
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
