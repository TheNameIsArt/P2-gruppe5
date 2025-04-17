using UnityEngine;

public class DirtyPlate : MonoBehaviour
{
    [Range(0f, 1f)] public float dirtiness = 1f; // 1 = fully dirty, 0 = clean
    public Sprite cleanSprite;
    public Sprite dirtySprite;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        UpdateVisual();
    }

    public void Clean(float amount)
    {
        dirtiness -= amount;
        dirtiness = Mathf.Clamp01(dirtiness);

        UpdateVisual();

        if (dirtiness <= 0f)
        {
            OnCleaned();
        }
    }

    void UpdateVisual()
    {
        // You can lerp between clean/dirty here, or use multiple frames for animation
        spriteRenderer.sprite = dirtiness < 0.5f ? cleanSprite : dirtySprite;
    }

    void OnCleaned()
    {
        // Optional: Add sparkle effect or sound
        Debug.Log($"{gameObject.name} is squeaky clean!");
    }
}