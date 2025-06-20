using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_MapShow : MonoBehaviour
{
    private GameObject map;
    private Scr_WorldMap scrMap;
    public GameObject player;
    private PlayerInput playerInput;
    private bool noMove = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        map = GameObject.Find("Map");
        scrMap = map.GetComponent<Scr_WorldMap>();
    }

    // Update is called once per frame
    void Update()
    {
        playerInput = player.GetComponent<PlayerInput>();
        if (Input.GetKeyDown(KeyCode.Escape)||Input.GetKeyDown(KeyCode.JoystickButton1)) 
        {
            Scr_WorldMap.isMapVisible = false; // Set the static variable to false when hiding the map
        }
    }
    public void PerformAction() 
    {
        Scr_WorldMap.isMapVisible = true; // Set the static variable to true when showing the map
        
    }
    
}
