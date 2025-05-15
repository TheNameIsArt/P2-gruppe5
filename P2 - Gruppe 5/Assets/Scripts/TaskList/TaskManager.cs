using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public static TaskManager Instance { get; private set; }

    public Transform taskListPanel;
    public GameObject taskItemPrefab;

    private List<TaskItem> currentTasks = new List<TaskItem>();

    // Tracks whether the task list is visible
    public bool IsTaskListVisible { get; private set; } = true;

    // Represents a task and its UI
    private class TaskItem
    {
        public GameObject taskObject;
        public TMP_Text taskText;
        public bool isCompleted;

        public TaskItem(GameObject obj, TMP_Text text)
        {
            taskObject = obj;
            taskText = text;
            isCompleted = false;
        }
    }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Uncomment the next line if you want the TaskManager to persist between scenes
        DontDestroyOnLoad(gameObject);
    }

    public void AddTask(string taskDescription)
    {
        GameObject newTask = Instantiate(taskItemPrefab, taskListPanel);
        TMP_Text textComponent = newTask.GetComponent<TMP_Text>();
        textComponent.text = taskDescription;
        currentTasks.Add(new TaskItem(newTask, textComponent));
    }

    // Call this to toggle a task's completion state
    public void ToggleTaskComplete(int index)
    {
        if (index < 0 || index >= currentTasks.Count) return;

        TaskItem task = currentTasks[index];
        task.isCompleted = !task.isCompleted;

        // Example: Strikethrough and color change for completed tasks
        if (task.isCompleted)
        {
            task.taskText.fontStyle = FontStyles.Strikethrough;
            task.taskText.color = Color.gray;
        }
        else
        {
            task.taskText.fontStyle = FontStyles.Normal;
            task.taskText.color = Color.white;
        }
    }

    public void ResetTasks()
    {
        // Destroy all task UI objects
        foreach (var task in currentTasks)
        {
            if (task.taskObject != null)
                Destroy(task.taskObject);
        }
        // Clear the list
        currentTasks.Clear();
    }

    public void RemoveTask(int index)
    {
        if (index < 0 || index >= currentTasks.Count) return;
        Destroy(currentTasks[index].taskObject);
        currentTasks.RemoveAt(index);
    }

    // --- New: Toggle task list UI visibility ---

    /// <summary>
    /// Toggles the visibility of the task list panel.
    /// </summary>
    public void ToggleTaskListVisibility()
    {
        IsTaskListVisible = !IsTaskListVisible;
        if (taskListPanel != null)
            taskListPanel.gameObject.SetActive(IsTaskListVisible);
    }

    /// <summary>
    /// Sets the visibility of the task list panel.
    /// </summary>
    public void SetTaskListVisibility(bool visible)
    {
        IsTaskListVisible = visible;
        if (taskListPanel != null)
            taskListPanel.gameObject.SetActive(visible);
    }
}
