using UnityEngine;
using UnityEngine.InputSystem;

public class BookScript : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public PlayerInput playerInput; // Reference to the shared PlayerInput component
    private int pageIndex = 0; // Index to track the current page

    private void OnEnable()
    {
        // Bind actions from the shared PlayerInput
        playerInput.actions["FlipForward"].performed += FlipForward;
        playerInput.actions["FlipBackwards"].performed += FlipBackwards;
    }

    private void OnDisable()
    {
        // Unbind actions to avoid memory leaks
        playerInput.actions["FlipForward"].performed -= FlipForward;
        playerInput.actions["FlipBackwards"].performed -= FlipBackwards;
    }

    public void FlipForward(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FlipForwardMouse();
        }
    }

    public void FlipBackwards(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            FlipBackwardsMouse();
        }
    }

    public void FlipForwardMouse()
    {
        if (animator != null && pageIndex < 3)
        {
            if (!IsAnimationPlaying("BookBack") && !IsAnimationPlaying("Book"))
            {
                animator.Play("Book", -1, 0f);
                pageIndex++;
            }
        }
    }

    public void FlipBackwardsMouse()
    {
        if (animator != null && pageIndex > 0)
        {
            if (!IsAnimationPlaying("BookBack") && !IsAnimationPlaying("Book"))
            {
                animator.Play("BookBack", -1, 0f);
                pageIndex--;
            }
        }
    }

    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        return currentState.IsName(animationName) && currentState.normalizedTime < 1f;
    }
}
