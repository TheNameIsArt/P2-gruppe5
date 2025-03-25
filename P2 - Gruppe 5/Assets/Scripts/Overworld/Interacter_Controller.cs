using UnityEngine;

public class Interacter_Controller : MonoBehaviour
{
    [SerializeField]
    private string targetSceneName;

    public GameObject interactButton;

    void Start()
    {
        
    }

    
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            //detect input method

            //Display appropriate button sprite

            //If player presses button, load target scene
        }
    }
}
