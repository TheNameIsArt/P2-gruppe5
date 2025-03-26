using System.Collections;
using UnityEngine;

public class LetterMover : MonoBehaviour
{
    public RectTransform letter;  // Reference to the letter's RectTransform
    public RectTransform targetBox;  // Reference to the target box's RectTransform
    public float riseSpeed = 300f;  // Speed at which the letter rises

    private Vector3 targetPosition;

    void Start()
    {
        // Set the target position to the box's position
        targetPosition = targetBox.position;
        // Start the letter movement
        StartCoroutine(RiseLetter());
    }

    IEnumerator RiseLetter()
    {
        // Start position is below the screen
        Vector3 startPosition = new Vector3(letter.position.x, -500f, letter.position.z);
        letter.position = startPosition;

        // Move letter to the target box position
        while (Vector3.Distance(letter.position, targetPosition) > 0.1f)
        {
            letter.position = Vector3.MoveTowards(letter.position, targetPosition, riseSpeed * Time.deltaTime);
            yield return null;
        }

        // Ensure it reaches the target exactly
        letter.position = targetPosition;
    }
}
    