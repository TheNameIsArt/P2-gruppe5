using UnityEngine;

public class CampPanel : MonoBehaviour
{
    public Sprite[] panel;
    private SpriteRenderer spriteRenderer;

    bool hovering;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = panel[0]; // Default sprite at start
    }

    void OnMouseEnter()
    {
        hovering = true;
        spriteRenderer.sprite = panel[1]; // Change to hover sprite
    }

    void OnMouseExit()
    {
        hovering = false;
        spriteRenderer.sprite = panel[0]; // Revert to default sprite
    }

    public void Select()
    {
        Debug.Log("Selected");
    }
}
