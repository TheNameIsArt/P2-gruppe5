using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public InputAction notePressedAction; 
    public GameObject goodHit, greatHit, perfectHit, missEffect;
    public float missedPositionY = -4f;
    public float pressWindowTime = 0.5f;
    private float noteAppearY = 5f; //Y position where the note should be visible
    private bool hasAppeared = false;
    private SpriteRenderer spriteRenderer;



    private float timeWhenActivated;

    void Start()
{
    timeWhenActivated = -1f;

    if (notePressedAction != null)
    {
        notePressedAction.Enable();
        notePressedAction.performed += ButtonPressed;
    }

    spriteRenderer = GetComponent<SpriteRenderer>();
    if (spriteRenderer != null)
    {
        spriteRenderer.enabled = false; // Start invisible
    }
}


    void Update()
{
    // Reveal the note once it reaches Y = appearY
    if (!hasAppeared && transform.position.y <= noteAppearY)
    {
        hasAppeared = true;
        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }
    }

    // Missed note check
    if (transform.position.y < missedPositionY)
    {
        if (timeWhenActivated >= 0 && Time.time - timeWhenActivated > pressWindowTime)
        {
            DjGameManager.instance.NoteMissed();
            Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            gameObject.SetActive(false);
        }
    }
}


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            Debug.Log("Entered activator!");
            canBePressed = true;
            timeWhenActivated = Time.time;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Activator"))
        {
            canBePressed = false;
        }
    }

    public void ButtonPressed(InputAction.CallbackContext context)
    {
       Debug.Log("Input performed!");

            if (canBePressed) 
        {
            gameObject.SetActive(false);

            if (Mathf.Abs(transform.position.y - (-2.6f)) > 0.25f)
            {
                Debug.Log("Good Hit!");
                DjGameManager.instance.GoodHit();
                Instantiate(goodHit, transform.position, goodHit.transform.rotation);
            }
            else if (Mathf.Abs(transform.position.y - (-2.6f)) > 0.10f)
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
