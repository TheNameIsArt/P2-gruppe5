using UnityEngine;

public class Scr_WorldMap : MonoBehaviour
{
    private Transform player;
    public SpriteRenderer spriteRenderer;
    public static bool isMapVisible = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithPlayer();
        if (isMapVisible) 
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 1f);
        }
        else if (!isMapVisible)
        {
            spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
        }
    }
    void MoveWithPlayer()
    {
        transform.position = new Vector3(player.position.x, player.position.y, player.position.z);
    }
}
