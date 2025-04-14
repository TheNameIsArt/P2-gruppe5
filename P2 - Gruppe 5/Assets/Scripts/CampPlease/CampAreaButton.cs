using UnityEngine;

public class CampAreaButton : MonoBehaviour
{
    public Sprite[] buttonStates; // [0] = default, [1] = selected
    private SpriteRenderer spriteRenderer;
    private CampPanel panel;

    private bool clicked = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = buttonStates[0];

        panel = Object.FindFirstObjectByType<CampPanel>();
    }

    void OnMouseEnter()
    {
        if (panel != null && panel.panelActivated && !clicked)
        {
            spriteRenderer.sprite = buttonStates[1];
        }
    }

    void OnMouseExit()
    {
        if (panel != null && panel.panelActivated && !clicked)
        {
            spriteRenderer.sprite = buttonStates[0];
        }
    }

    void OnMouseDown()
    {
        if (panel != null && panel.panelActivated)
        {
            clicked = !clicked; // toggle
            spriteRenderer.sprite = clicked ? buttonStates[1] : buttonStates[0];
        }
    }
}
