using UnityEngine;

public class samQuestChecker : MonoBehaviour
{
    // Call this to toggle a quest by name
    public void TriggerQuest(string questName)
    {
        samQuest.Instance.ToggleQuest(questName);
    }
}
