using UnityEngine;

public class Scr_SpriteLayering : MonoBehaviour
{
    public GameObject topLayer;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        topLayer.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            topLayer.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            topLayer.SetActive(false);
        }
    }
}
