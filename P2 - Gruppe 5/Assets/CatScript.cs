using UnityEngine;
using DialogueEditor;
using System.Collections;

public class CatScript : MonoBehaviour
{
    public NPCConversation catStart;
    private static bool hasEnteredScene = false;
    [SerializeField] private GameObject convoZone; // Reference to the cat prefab

    // New fields for smooth movement
    [SerializeField] private float moveLeftDistance = 5f; // Customizable distance
    [SerializeField] private float moveDuration = 1f; // Duration of the movement

    private bool hasMoved = false; // Flag to ensure movement happens only once

    void Start()
    {
        if (!hasEnteredScene)
        {
            hasEnteredScene = true;
            Debug.Log("This happens only the first time you enter the scene.");
            ConversationManager.Instance.StartConversation(catStart);
            convoZone.SetActive(false); // Deactivate convo zone at the start
        }
    }

    private void Update()
    {
        if (!ConversationManager.Instance.IsConversationActive && hasEnteredScene && !hasMoved)
        {
            // Move the cat smoothly to the left
            StartCoroutine(MoveCatLeftAndDespawn());
            hasMoved = true; // Set the flag to true to prevent further movement
        }
    }

    // Coroutine to move the cat smoothly to the left and despawn it
    private IEnumerator MoveCatLeftAndDespawn()
    {
        Vector3 startPosition = transform.position;
        Vector3 targetPosition = startPosition + Vector3.left * moveLeftDistance;

        float elapsedTime = 0f;

        while (elapsedTime < moveDuration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        transform.position = targetPosition; // Ensure the final position is exact

        Debug.Log("Cat moved to the left.");
        convoZone.SetActive(true); // active convo zone
    }
}
