using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    public KeyCode keyToPress;
    public GameObject goodHit, greatHit, perfectHit, missEffect;
    public float missedPositionY = -4f;
    public float pressWindowTime = 0.5f;

    private float timeWhenActivated;
    private static bool hasStartedMusic = false;

    void Start()
    {
        timeWhenActivated = -1f; 
    }

    void Update()
    {
        if (canBePressed && Input.GetKeyDown(keyToPress))
        {
            // Start the music from the GameManager if it's the first note hit
            if (!hasStartedMusic)
            {
                DjGameManager.instance.StartMusic();
                hasStartedMusic = true;
            }

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
        if (other.tag == "Activator")
        {
            canBePressed = true;
            timeWhenActivated = Time.time;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;
        }
    }
}
