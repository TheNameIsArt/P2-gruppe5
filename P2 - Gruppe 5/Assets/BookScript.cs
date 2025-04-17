using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class BookScript : MonoBehaviour
{
    public Animator animator; // Reference to the Animator component
    public PlayerInput playerInput; // Reference to the shared PlayerInput component
    public TMP_Text pageText; // Reference to the TMP text for the page number
    public SpriteRenderer pageRenderer; // Reference to the SpriteRenderer for the page
    public Sprite[] pageSprites; // Array of sprites for the pages

    private int pageIndex = 0; // Index to track the current page
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
        if (animator != null && pageIndex < pageSprites.Length - 1)
        {
            if (!IsAnimationPlaying("BookBack") && !IsAnimationPlaying("Book"))
            {
                DisablePageSprite();
                DisablePageText();
                animator.Play("Book", -1, 0f);
                pageIndex++;
                float animationDuration = GetAnimationDuration("Book") + additionalDelay;
                Invoke(nameof(UpdatePageSpriteForward), animationDuration);
                Invoke(nameof(EnablePageSprite), animationDuration);
                Invoke(nameof(EnablePageText), animationDuration);
            }
        }
    }

    public void FlipBackwardsMouse()
    {
        if (animator != null && pageIndex > 0)
        {
            if (!IsAnimationPlaying("BookBack") && !IsAnimationPlaying("Book"))
            {
                DisablePageSprite();
                DisablePageText();
                animator.Play("BookBack", -1, 0f);
                pageIndex--;
                float animationDuration = GetAnimationDuration("BookBack") + additionalDelay;
                Invoke(nameof(UpdatePageSpriteBackward), animationDuration);
                Invoke(nameof(EnablePageSprite), animationDuration);
                Invoke(nameof(EnablePageText), animationDuration);
            }
        }
    }

    private void DisablePageSprite()
    {
        if (pageRenderer != null) pageRenderer.enabled = false;
    }

    private void EnablePageSprite()
    {
        if (pageRenderer != null) pageRenderer.enabled = true;
    }

    private void DisablePageText()
    {
        if (pageText != null) pageText.gameObject.SetActive(false);
    }

    private void EnablePageText()
    {
        if (pageText != null) pageText.gameObject.SetActive(true);
    }

    private void UpdatePageSpriteForward()
    {
        // Update the page sprite for flipping forward
        if (pageRenderer != null)
        {
            pageRenderer.sprite = pageSprites[pageIndex];
        }

        // Update the page number
        UpdatePageText(pageIndex + 1);
    }

    private void UpdatePageSpriteBackward()
    {
        // Update the page sprite for flipping backward
        if (pageRenderer != null)
        {
            pageRenderer.sprite = pageSprites[pageIndex];
        }

        // Update the page number
        UpdatePageText(pageIndex + 1);
    }

    private void UpdatePageText(int pageNumber)
    {
        if (pageText != null) pageText.text = pageNumber.ToString();
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
