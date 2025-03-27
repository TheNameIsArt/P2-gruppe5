using UnityEngine;

public class NoteSpawnerScript : MonoBehaviour
{
    public GameObject[] keys; // Different note prefabs, one for each lane
    public Transform[] spawnPoints; // Assign spawn positions in the Inspector
    public float bpm = 130f; // Set BPM of the song
    public AudioSource musicSource; // Reference to the song audio

    private float secondsPerBeat;
    private float songStartTime;
    private int beatIndex = 0; // Keeps track of beats

    void Start()
    {
        if (bpm <= 0)
        {
            Debug.LogError("BPM must be greater than zero! Setting default BPM to 130.");
            bpm = 130f; // Default BPM to prevent crashes
        }

        secondsPerBeat = 60f / bpm; // Time between beats
        Debug.Log("Seconds per beat: " + secondsPerBeat); // Log the seconds per beat to check
        songStartTime = Time.time; // Mark the start of the song
    }

    void Update()
    {
        float songPosition = Time.time - songStartTime; // Time elapsed in song
        if (secondsPerBeat > 0 && songPosition >= beatIndex * secondsPerBeat)
        {
            SpawnKey(); // Spawn key in a random lane
            beatIndex++; // Move to the next beat
        }
        else if (secondsPerBeat <= 0)
        {
            Debug.LogError("secondsPerBeat is zero or negative, which is invalid.");
        }
    }

    void SpawnKey()
    {
        // Randomly select a prefab (note) to spawn
        int randomNoteIndex = Random.Range(0, keys.Length);

        // Ensure that each note stays in its own lane (spawn at the corresponding spawn point)
        if (randomNoteIndex < spawnPoints.Length) // Ensure the lane exists
        {
            // Instantiate the note at the selected spawn point
            Instantiate(keys[randomNoteIndex], spawnPoints[randomNoteIndex].position, Quaternion.identity);
            Debug.Log("Spawning note in lane: " + randomNoteIndex);
        }
        else
        {
            Debug.LogError("Invalid random note index: " + randomNoteIndex);
        }
    }
}
