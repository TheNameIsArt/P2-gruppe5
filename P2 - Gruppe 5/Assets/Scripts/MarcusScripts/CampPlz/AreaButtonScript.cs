using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Image))]
public class ButtonSpriteSelector : MonoBehaviour, ISelectHandler, IDeselectHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Sprite normalSprite;
    public Sprite highlightedSprite;

    private Image buttonImage;
    private bool isSelected = false;
    private bool isHovered = false;

    void Awake()
    {
        buttonImage = GetComponent<Image>();
        if (buttonImage && normalSprite == null)
            normalSprite = buttonImage.sprite;
    }

    void UpdateSprite()
    {
        // Update the button's image
        if (buttonImage != null)
        {
            if (isSelected || isHovered)
                buttonImage.sprite = highlightedSprite;
            else
                buttonImage.sprite = normalSprite;
        }

        // Iterate through all child GameObjects to find the desired one
        foreach (Transform child in transform.GetComponentsInChildren<Transform>())
        {
            // Find a child with spriterenderer component
            SpriteRenderer spriteRenderer = child.GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                // Update the sprite in the SpriteRenderer
                if (isSelected || isHovered)
                    spriteRenderer.sprite = highlightedSprite;
                else
                    spriteRenderer.sprite = normalSprite;

                // Break if only one child needs to be updated
                break;
            }
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        isSelected = true;
        UpdateSprite();
    }

    public void OnDeselect(BaseEventData eventData)
    {
        isSelected = false;
        UpdateSprite();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        Debug.Log("Pointer entered");
        isHovered = true;
        UpdateSprite();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        Debug.Log("Pointer exited");
        isHovered = false;
        UpdateSprite();
    }
}
