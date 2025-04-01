using UnityEngine;

public class NoteObject : MonoBehaviour
{
    public bool canBePressed;
    private bool wasHit; // Track whether the note was hit or missed
    public KeyCode keyToPress;
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    void Update()
    {
        if (Input.GetKeyDown(keyToPress) && canBePressed)
        {
            wasHit = true; // Marks the note as hit
            gameObject.SetActive(false);

            // Start music if it's the first note pressed
            if (!DjGameManager.instance.IsMusicPlaying)
            {
                DjGameManager.instance.StartMusic();
            }

            float distanceToHitZone = Mathf.Abs(transform.position.y - (-3)); // Distance from y = -3

            if (distanceToHitZone > 0.25f)
            {
                DjGameManager.instance.NormalHit();
                Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
            }
            else if (distanceToHitZone > 0.05f)
            {
                DjGameManager.instance.GoodHit();
                Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
            }
            else
            {
                DjGameManager.instance.PerfectHit();
                Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Activator")
        {
            canBePressed = false;
            if (!wasHit) // Miss if note was not hit
            {
                DjGameManager.instance.NoteMissed();
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
            Destroy(gameObject); // Removes the note after it leaves the hit zone
        }
    }
}
