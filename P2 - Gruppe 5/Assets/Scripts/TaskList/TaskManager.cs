using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TaskManager : MonoBehaviour
{
    public Transform taskListPanel;
    public GameObject taskItemPrefab;

    private List<GameObject> currentTasks = new List<GameObject>();

    public void AddTask(string taskDescription)
    {
        GameObject newTask = Instantiate(taskItemPrefab, taskListPanel);
        newTask.GetComponent<TMP_Text>().text = taskDescription; // Or .GetComponent<TMP_Text>() for TextMeshPro
        currentTasks.Add(newTask);
    }

    // Optional: Remove task, clear list, etc.
}
