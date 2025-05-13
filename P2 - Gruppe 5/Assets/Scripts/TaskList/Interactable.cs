using UnityEngine;

public class Interactable : MonoBehaviour
{
    public string taskToAdd;
    public TaskManager taskManager;

    void Interact()
    {
        if (taskManager != null)
        {
            taskManager.AddTask(taskToAdd);
        }
    }

    // You can call Interact() via a trigger, button, or raycast
}
