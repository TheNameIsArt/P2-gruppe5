using UnityEngine;

public class Scr_MapHide : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Scr_WorldMap.isMapVisible = false; // Set the static variable to false when hiding the map
    }
}
