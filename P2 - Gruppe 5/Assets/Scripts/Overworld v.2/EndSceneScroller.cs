using UnityEngine;

public class EndSceneScroller : MonoBehaviour
{
    public Rigidbody2D rb;
    private float scrollSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        scrollSpeed = 1;
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocityY = scrollSpeed;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "scrollStop") 
        {
            scrollSpeed = 0;
        }
    }
}
