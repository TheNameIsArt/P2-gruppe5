using UnityEngine;

public class DisableByTag : MonoBehaviour
{
    // Set this in the Inspector or via script
    public string targetTag = "TargetTag"; // Replace with the tag you want to disable

    // Call this method to disable the GameObject with the specified tag
    public void DisableObjectByTag()
    {
        GameObject targetObject = GameObject.FindWithTag(targetTag);

        if (targetObject != null)
        {
            targetObject.SetActive(false);
            Debug.Log($"GameObject with tag '{targetTag}' has been disabled.");
        }
        else
        {
            Debug.LogWarning($"No GameObject found with tag '{targetTag}'.");
        }
    }
}
