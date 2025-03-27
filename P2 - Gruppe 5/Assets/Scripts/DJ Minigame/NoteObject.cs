using UnityEngine;

public class NoteObject : MonoBehaviour
{

    public bool canBePressed;

    public KeyCode keyToPress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(keyToPress) && canBePressed)
        {
            gameObject.SetActive(false);

            DjGameManager.instance.NoteHit();
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag =="Activator")
        {
            canBePressed = true;
        }
    }


    private void OnTriggerExit2D(Collider2D other)
    {
        if(other.tag =="Activator")
        {
            canBePressed = false;

            DjGameManager.instance.NoteMissed();
        }
    }
}
