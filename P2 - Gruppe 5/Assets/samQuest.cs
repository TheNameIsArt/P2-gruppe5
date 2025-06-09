using DialogueEditor;
using UnityEngine;

public class samQuest : MonoBehaviour
{
    public static samQuest Instance { get; private set; }

    public bool burgerBar = false;
    public bool lostAndFound = false;
    public bool buckActive = false;
    static bool SamStarted = false;
    public NPCConversation startConvo;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Prevent duplicates
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject); // Optional: persists across scenes
    }

    void Start()
    {
        if (!SamStarted)
        {
            SamStarted = true;
            Debug.Log("samQuest started");
            ConversationManager.Instance.StartConversation(startConvo);
        }
    }
    public void SetQuest(string questName, bool value)
    {
        switch (questName)
        {
            case "burgerBar":
                burgerBar = value;
                break;
            case "lostAndFound":
                lostAndFound = value;
                break;
            case "buckActive":
                buckActive = value;
                break;
            default:
                Debug.LogWarning("Unknown quest: " + questName);
                break;
        }
    }
    public void ToggleQuest(string questName)
    {
        switch (questName)
        {
            case "burgerBar":
                burgerBar = !burgerBar;
                break;
            case "lostAndFound":
                lostAndFound = !lostAndFound;
                break;
            case "buckActive":
                buckActive = !buckActive;
                break;
            default:
                Debug.LogWarning("Unknown quest: " + questName);
                break;
        }
    }
    public bool IsQuestActive(string questName)
    {
        switch (questName)
        {
            case "burgerBar": return burgerBar;
            case "lostAndFound": return lostAndFound;
            case "buckActive": return buckActive;
            default:
                Debug.LogWarning("Unknown quest: " + questName);
                return false;
        }
    }
}
