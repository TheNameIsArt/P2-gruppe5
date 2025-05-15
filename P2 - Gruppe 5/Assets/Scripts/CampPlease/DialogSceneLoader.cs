using UnityEngine;
using DialogueEditor;
using UnityEngine.SceneManagement;

public abstract class DialogSceneLoader : MonoBehaviour
{
    public NPCConversation DialogConvo;
    protected static bool InfomationStart = false;
    public string TriggerFromSceneName; // Name of the scene that should trigger dialog

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    protected virtual void Start()
    {
        // Store the current scene name for the next scene
        PlayerPrefs.SetString("PreviousScene", SceneManager.GetActiveScene().name);
        PlayerPrefs.Save();

        string previousScene = PlayerPrefs.GetString("PreviousScene", "");
        if (!InfomationStart && previousScene == TriggerFromSceneName)
        {
            InfomationStart = true;
            StartDialog();
        }
        else if (!InfomationStart)
        {
            InfomationStart = true; // Set to true only the first time Start runs
            gameObject.SetActive(false);
        }
    }

    protected virtual void StartDialog()
    {
        if (DialogConvo != null)
        {
            ConversationManager.Instance.StartConversation(DialogConvo);
        }
    }

    public virtual void StartGame()
    {
        InfomationStart = true;
    }
}