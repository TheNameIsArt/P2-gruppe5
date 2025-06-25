using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public InputAction notePressedAction; 
    public GameObject goodHit, greatHit, perfectHit, missEffect; // Prefabs for each hit effect
    private float missedPositionY = -2.25f;
    private float pressWindowTime = 0.5f; // The time that the note is presseable
    private float noteAppearY = 5f; //Y position where the note should be visible
    private bool hasAppeared = false;
    private SpriteRenderer spriteRenderer;
    public InputDevice inputDevice;
    private float hitboxYPosition = -1.36f; //This is where the note can be hit, according to the y-position
    private float goodHitYPosition = 0.25f; //Distance from box which registers a good hit
    private float greatHitYPosition = 0.10f; //Distance from box which registers a great hit


    private float timeWhenActivated; 

    void Start()
{
    timeWhenActivated = -1f; //By making the time -1, the note is not being registered. 

    if (notePressedAction != null)
    {
        notePressedAction.Enable();
        notePressedAction.performed += ButtonPressed;
    }

    spriteRenderer = GetComponent<SpriteRenderer>(); // Finds the SpriteRenderer
    if (spriteRenderer != null)
    {
        spriteRenderer.enabled = false; // hides notes at the start, saving process power.
    }
}


    void Update()
{
    if (!hasAppeared && transform.position.y <= noteAppearY)
    {
        hasAppeared = true;
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true; // Shows the note once it enters the visible area
        }
    }

    // Missed note check
    if (transform.position.y < missedPositionY)
    {
        if (timeWhenActivated >= 0 && Time.time - timeWhenActivated > pressWindowTime) //If the note was activated and enough time passed, a miss is counted
        {
            DjGameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            gameObject.SetActive(false); //Disables the note
        }
    }
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            Debug.Log("Entered activator!");
            canBePressed = true; //Once the note enters the collider, it can be pressed
            timeWhenActivated = Time.time;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false; //Once the note leaves the collider it can no longer be pressed
        }
    }

    public void ButtonPressed(InputAction.CallbackContext context)
    {
        Debug.Log("Input performed!");

        if (canBePressed)
        {
            gameObject.SetActive(false); //Deactivates the note after being pressed

            //Based on the y-position from the hitbox, a hit effect is registered
            if (Mathf.Abs(transform.position.y - (hitboxYPosition)) > goodHitYPosition)
            {
                Debug.Log("Good Hit!");
                DjGameManager.instance.GoodHit();
                Instantiate(goodHit, transform.position, goodHit.transform.rotation);
            }
            else if (Mathf.Abs(transform.position.y - (hitboxYPosition)) > greatHitYPosition)
            {
                Debug.Log("Great Hit!");
                DjGameManager.instance.GreatHit();
                Instantiate(greatHit, transform.position, greatHit.transform.rotation);
            }
            else
            {
                Debug.Log("Perfect Hit!");
                DjGameManager.instance.PerfectHit();
                Instantiate(perfectHit, transform.position, perfectHit.transform.rotation);
            }
        }
    }

    private void OnDestroy()
    {
        notePressedAction.performed -= ButtonPressed;
        notePressedAction.Disable();       
    }
}
