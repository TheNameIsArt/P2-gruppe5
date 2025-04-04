using UnityEngine;

public class TempoScroller : MonoBehaviour
{
    
    public float musicTempo = 125f;

    public bool hasStarted; //Knapperne bevæger sig automatisk når hasStarted = true. Ellers skal man kligge for at de bevæger sig
    void Start()
    {
        musicTempo = musicTempo / 60f;
        Application.targetFrameRate = 60;
    }
    
   void Update()
    {
        if (hasStarted)
        {
            transform.position -= new Vector3(0f, musicTempo * Time.deltaTime, 0f);
        }
    }
}
