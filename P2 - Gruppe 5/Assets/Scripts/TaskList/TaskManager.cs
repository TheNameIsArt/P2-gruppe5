using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Transform taskListPanel;
    public GameObject taskItemPrefab;

    private List<TaskItem> currentTasks = new List<TaskItem>();

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

    private void Start()
    {
        // Example tasks
        AddTask("Collect 10 coins");
        AddTask("Talk to the NPC");
        AddTask("Find the hidden treasure");
    }

}
