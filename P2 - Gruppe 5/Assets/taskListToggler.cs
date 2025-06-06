using UnityEngine;

public class taskListToggler : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        TaskManager.Instance.ToggleTaskListVisibility();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
