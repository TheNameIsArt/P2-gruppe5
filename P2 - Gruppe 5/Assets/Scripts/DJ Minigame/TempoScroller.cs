using UnityEngine;

public class TempoScroller : MonoBehaviour
{

    public float musicTempo;

    public bool hasStarted; //Knapperne bevæger sig automatisk når hasStarted = true. Ellers skal man kligge for at de bevæger sig
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicTempo = musicTempo / 62.5f; //Det halve af BPM(?)
    }

   void Update()
    {

        if (hasStarted)
        {
            transform.position -= new Vector3(0f, musicTempo * Time.deltaTime, 0f);
        }
    }
}
