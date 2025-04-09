using UnityEngine;
using UnityEngine.InputSystem;

public class NoteObject : MonoBehaviour
{
    /*public bool canBePressed;
    public InputAction notePressAction; // Reference to the InputAction
    public GameObject goodHit, greatHit, perfectHit, missEffect;
    public float missedPositionY = -4f;
    public float pressWindowTime = 0.5f;

    private float timeWhenActivated;

    void Start()
    {
        timeWhenActivated = -1f;

        // Enable the InputAction
        if (notePressAction != null)
        {
            notePressAction.Enable();
            notePressAction.performed += OnNotePressed;
        }
    }

    void Update()
    {
        // Check if the note's Y position is less than or equal to -2.6
        if (transform.position.y <= -2.5f && !DjGameManager.instance.HasStartedMusic)
        {
            DjGameManager.instance.StartMusic();
            DjGameManager.instance.HasStartedMusic = true;
            Debug.Log("Music Started");
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

    private void OnNotePressed(InputAction.CallbackContext context)
    {
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
        // Disable the InputAction and unsubscribe from the event
        if (notePressAction != null)
        {
            notePressAction.performed -= OnNotePressed;
            notePressAction.Disable();
        }
    }*/
}