using UnityEngine;

public class CampButton : MonoBehaviour
{
    public Sprite[] buttonStates;
    private SpriteRenderer spriteRenderer;
    private CampPanel panel;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = buttonStates[0];

        panel = Object.FindFirstObjectByType<CampPanel>();
    }

    void OnMouseEnter()
    {
        if (panel != null && panel.panelActivated) // Only change sprite if the panel is activated
        {
            spriteRenderer.sprite = buttonStates[1];
        }
    }

    void OnMouseExit()
    {
        if (panel != null && panel.panelActivated) // Only reset sprite if the panel is activated
        {
            spriteRenderer.sprite = buttonStates[0];
        }
    }

    void OnMouseDown()
    {
        if (panel != null && panel.panelActivated) // Only interact if the panel is activated
        {
            spriteRenderer.sprite = buttonStates[1];
        }
    }
}
