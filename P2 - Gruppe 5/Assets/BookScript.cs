using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BookScript : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public PlayerInput playerInput; // Reference to the shared PlayerInput component
    public TMP_Text leftPageText; // Reference to the TMP text for the left page
    public TMP_Text rightPageText; // Reference to the TMP text for the right page

    private int pageIndex = 0; // Index to track the current page
    private int leftPageCount = 1; // Count of pages to display
    private int rightPageCount = 2; // Count of pages to display
    [SerializeField] private float additionalDelay = 0.3f; // Additional delay in seconds

    private void OnEnable()
    {
        playerInput.actions["FlipForward"].performed += FlipForward;
        playerInput.actions["FlipBackwards"].performed += FlipBackwards;
    }

    private void OnDisable()
    {
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
                DisablePageTexts();
                animator.Play("Book", -1, 0f);
                pageIndex++;
                float animationDuration = GetAnimationDuration("Book") + additionalDelay;
                Invoke(nameof(UpdatePageTextsForward), animationDuration);
                Invoke(nameof(EnablePageTexts), animationDuration);
            }
        }
    }

    public void FlipBackwardsMouse()
    {
        if (animator != null && pageIndex > 0)
        {
            if (!IsAnimationPlaying("BookBack") && !IsAnimationPlaying("Book"))
            {
                DisablePageTexts();
                animator.Play("BookBack", -1, 0f);
                pageIndex--;
                float animationDuration = GetAnimationDuration("BookBack") + additionalDelay;
                Invoke(nameof(UpdatePageTextsBackward), animationDuration);
                Invoke(nameof(EnablePageTexts), animationDuration);
            }
        }
    }

    private void DisablePageTexts()
    {
        if (leftPageText != null) leftPageText.gameObject.SetActive(false);
        if (rightPageText != null) rightPageText.gameObject.SetActive(false);
    }

    private void EnablePageTexts()
    {
        if (leftPageText != null) leftPageText.gameObject.SetActive(true);
        if (rightPageText != null) rightPageText.gameObject.SetActive(true);
    }

    private void UpdatePageTextsForward()
    {
        // Increment both page numbers by 2
        leftPageCount += 2;
        rightPageCount += 2;
        UpdatePageTexts(leftPageCount, rightPageCount);
    }

    private void UpdatePageTextsBackward()
    {
        // Decrement both page numbers by 2
        leftPageCount -= 2;
        rightPageCount -= 2;
        UpdatePageTexts(leftPageCount, rightPageCount);
    }

    private void UpdatePageTexts(int leftPage, int rightPage)
    {
        if (leftPageText != null) leftPageText.text = leftPage.ToString();
        if (rightPageText != null) rightPageText.text = rightPage.ToString();
    }

    private float GetAnimationDuration(string animationName)
    {
        foreach (AnimationClip clip in animator.runtimeAnimatorController.animationClips)
        {
            if (clip.name == animationName)
            {
                return clip.length;
            }
        }
        Debug.LogWarning($"Animation '{animationName}' not found in Animator.");
        return 0f;
    }

    private bool IsAnimationPlaying(string animationName)
    {
        AnimatorStateInfo currentState = animator.GetCurrentAnimatorStateInfo(0);
        return currentState.IsName(animationName) && currentState.normalizedTime < 1f;
    }
}
