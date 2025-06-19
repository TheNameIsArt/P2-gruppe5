using UnityEngine;

public class samQuestItems : MonoBehaviour
{
    public string questName; // Set this in the Inspector

    void Start()
    {
        StartCoroutine(WaitAndCheckQuest());
    }

    private System.Collections.IEnumerator WaitAndCheckQuest()
    {
        yield return null; // Waits one frame
        if (!samQuest.Instance.IsQuestActive(questName))
        {
            gameObject.SetActive(false);
        }
    }
}
