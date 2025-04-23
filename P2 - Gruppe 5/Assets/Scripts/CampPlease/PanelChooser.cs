using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PanelChooser : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler, IDeselectHandler
{
    [Header("References")]
    public Button targetButton; // The button to select when this button is pressed
    public GameObject spriteTarget; // The GameObject whose sprite will change
    public Sprite hoverSprite; // The sprite to display on hover or selection
    public Sprite defaultSprite; // The default sprite

    private Button thisButton;
    private SpriteRenderer spriteTargetRenderer; // Reference to the SpriteRenderer
    public PanelManager panelManager; // Reference to the PanelManager

    public PlayerInput playerInput; // Reference to the PlayerInput component

    void Start()
    {
        // Cache references
        thisButton = GetComponent<Button>();
        if (spriteTarget != null)
        {
            spriteTargetRenderer = spriteTarget.GetComponent<SpriteRenderer>();
        }

        // Add listener for button click
        if (thisButton != null)
        {
            thisButton.onClick.AddListener(OnButtonClick);
        }
    }

    void Update()
    {
        // Ensure the button remains selected when the "Cancel" action is triggered
        if (playerInput != null && playerInput.actions["Cancel"].triggered)
        {
            EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }

    void OnEnable()
    {
        // Reset the sprite to the default when the GameObject is reactivated
        if (spriteTargetRenderer != null && defaultSprite != null)
        {
            spriteTargetRenderer.sprite = defaultSprite;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        // Change sprite on hover
        if (spriteTargetRenderer != null && hoverSprite != null)
        {
            spriteTargetRenderer.sprite = hoverSprite;
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        // Revert to default sprite when not hovering
        if (spriteTargetRenderer != null && defaultSprite != null)
        {
            spriteTargetRenderer.sprite = defaultSprite;
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        // Change sprite when the button is selected via the Event System
        if (spriteTargetRenderer != null && hoverSprite != null)
        {
            spriteTargetRenderer.sprite = hoverSprite;
        }
    }

    public void OnDeselect(BaseEventData eventData)
    {
        // Revert to default sprite when the button is deselected
        if (spriteTargetRenderer != null && defaultSprite != null)
        {
            spriteTargetRenderer.sprite = defaultSprite;
        }
    }

    private void OnButtonClick()
    {
        panelManager.UpdateLastSelectedPanel(gameObject); // Update the last selected panel in the PanelManager
        if (targetButton != null)
        {
            // Select the target button
            EventSystem.current.SetSelectedGameObject(targetButton.gameObject);
        }
        else
        {
            // Log a warning if the target button is not set
            Debug.LogWarning("Target button is not assigned in the PanelChooser script.", this);
        }
    }
}
