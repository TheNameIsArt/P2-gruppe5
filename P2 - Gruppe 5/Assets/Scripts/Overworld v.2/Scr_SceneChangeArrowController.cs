using UnityEngine;

public class Scr_SceneChangeArrowController : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    private GameObject arrow;
    private void Awake()
    {
        // Find the "Arrow" child object within this instance of the prefab
        arrow = transform.Find("Arrow")?.gameObject;

        // Ensure the "Arrow" object starts as inactive
        if (arrow != null)
        {
            arrow.SetActive(false);
        }
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (arrow != null)
            {
                arrow.SetActive(true);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (arrow != null)
            {
                arrow.SetActive(false);
                
            }
                
        }
    }
}
