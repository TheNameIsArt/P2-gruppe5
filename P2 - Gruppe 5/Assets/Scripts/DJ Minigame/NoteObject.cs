using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    public GameObject goodHit, greatHit, perfectHit, missEffect;
    public float missedPositionY = -4f;
    public float pressWindowTime = 0.5f;

    private float timeWhenActivated;

    void Start()
    {
        timeWhenActivated = -1f;
    }

    void Update()
    {
        // Check if the note's Y position is less than or equal to -2.6
        if (transform.position.y <= -2.5f && !DjGameManager.instance.HasStartedMusic) //Tester pt. med 2.5
        {
            DjGameManager.instance.StartMusic();  // Start the music
            DjGameManager.instance.HasStartedMusic = true;  // Mark that music has started
            Debug.Log("Music Started");  // Debugging output to confirm music starts
        }

        // Check for input when the note can be pressed
        if (canBePressed && Input.GetKeyDown(keyToPress))
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
}
