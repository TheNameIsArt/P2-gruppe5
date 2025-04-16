using UnityEngine;
using UnityEngine.InputSystem;

public class BookScript : MonoBehaviour
{
    [Header("References")]
    public Animator animator; // Reference to the Animator component
    public void FlipForward(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("FlipForward triggered");
            if (animator != null)
            {
                animator.speed = 1f; // Set the animator speed to normal
                animator.Play("Book");
                Debug.Log("Playing 'Book' animation forward");
            }
            else
            {
                Debug.LogError("Animator is not assigned!");
            }
        }
    }

    public void FlipBackwards(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Debug.Log("FlipBackwards triggered");
            if (animator != null)
            {
                animator.speed = -1f; // Set the animator speed to reverse
                animator.Play("Book");
                Debug.Log("Playing 'Book' animation backward");
            }
            else
            {
                Debug.LogError("Animator is not assigned!");
            }
        }
    }
}
