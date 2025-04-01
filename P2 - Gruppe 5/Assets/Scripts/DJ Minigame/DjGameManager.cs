using UnityEngine;

public class DjGameManager : MonoBehaviour
{

    public AudioSource music;
    public bool startPlaying;
    public TempoScroller tempoScroller;

    public static DjGameManager instance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NoteHit()
    {
        Debug.Log("Hit on time");
    }

    public void NoteMissed()
    {
        Debug.Log("Missed note");
    }
}
