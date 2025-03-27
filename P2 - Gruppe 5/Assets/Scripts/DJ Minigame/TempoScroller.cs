using UnityEngine;

public class TempoScroller : MonoBehaviour
{

    public float musicTempo;

    public bool hasStarted; //Knapperne bevæger sig automatisk når hasStarted = true. Ellers skal man kligge for at de bevæger sig
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        musicTempo = musicTempo / 60f;
    }

    // Update is called once per frame
   void Update()
    {
        if (!hasStarted)
        {
            /*if (Input.anyKeyDown)
            {
                hasStarted = true;
            }*/
        }

        if (hasStarted)
        {
            transform.position -= new Vector3(0f, musicTempo * Time.deltaTime, 0f);
        }
    }
}
