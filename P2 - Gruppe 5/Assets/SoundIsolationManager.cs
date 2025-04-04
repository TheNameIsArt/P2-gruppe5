using UnityEngine;

public class SoundIsolationManager : MonoBehaviour
{
    [SerializeField] GameObject[] soundWaves;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        soundWaves = GameObject.FindGameObjectsWithTag("SoundWaves");
    }

    // Update is called once per frame
    void Update()
    {
        CheckSoundWaves();
    }

    void CheckSoundWaves()
    {
        bool allOff = true;
        foreach (var soundWave in soundWaves)
        {
            if (soundWave.activeSelf)
            {
                allOff = false;
                break;
            }
        }

        if (allOff)
        {
            OnAllSoundWavesOff();
        }
    }

    void OnAllSoundWavesOff()
    {
        // Add your logic here for when all sound waves are turned off
        Debug.Log("All sound waves are turned off!");

    }
}
