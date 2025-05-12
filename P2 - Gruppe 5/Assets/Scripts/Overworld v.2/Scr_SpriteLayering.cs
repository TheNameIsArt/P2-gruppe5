using UnityEngine;

public class Scr_SpriteLayering : MonoBehaviour
{
    public GameObject topLayer;
    public GameObject secondLayer;
    public GameObject thirdLayer;
    public bool needsSecondLayer;
    public bool needsThirdLayer;

    private SpriteRenderer spriteRenderer;
    private SpriteRenderer secondSpriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = topLayer.GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

        if (needsSecondLayer)
        {
            secondSpriteRenderer = secondLayer.GetComponent<SpriteRenderer>();
            secondSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }
        if (needsThirdLayer)
        {
            thirdLayer.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            if (needsSecondLayer)
            {
                secondSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);
            }
           if (needsThirdLayer)
            {
                thirdLayer.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

            if (needsSecondLayer) 
            {
                secondSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);
            }
            if (needsThirdLayer)
            {
                thirdLayer.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 0f);
            }
        }
    }
}
