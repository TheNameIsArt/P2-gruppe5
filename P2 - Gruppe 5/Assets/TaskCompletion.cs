using UnityEngine;

public class TaskCompletion : MonoBehaviour
{
    // Adds a new task to the TaskManager
    public void AddTaskToManager(string description)
    {
        if (TaskManager.Instance != null)
            TaskManager.Instance.AddTask(description);
    }

    // Removes a task from the TaskManager by index
    public void ToggleTaskComplete(int index)
    {
        if (TaskManager.Instance != null)
            TaskManager.Instance.ToggleTaskComplete(index);
    }
    public void ResetTasks()
    {
        if (TaskManager.Instance != null)
            TaskManager.Instance.ResetTasks();
    }
    // Removes a task from the TaskManager by index
    public void RemoveTaskFromManager(int index)
    {
        if (TaskManager.Instance != null)
            TaskManager.Instance.RemoveTask(index);
    }
}
