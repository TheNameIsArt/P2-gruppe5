using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.Linq;

public class NoteSpawnerScript : MonoBehaviour
{
    public GameObject[] keys; // Different note prefabs, one for each lane
    public Transform[] spawnPoints; // Assign spawn positions in the Inspector
    public TextAsset midiFile;

    private List<NoteData> notes = new List<NoteData>();
    private int noteIndex = 0;
    private float songStartTime;

    void Start()
    {
        if (midiFile == null)
        {
            Debug.LogError("MIDI file is missing!");
            return;
        }

        ReadMidiFile(); // Extract note timing from MIDI
        songStartTime = Time.time; // Mark the start of the song
    }

    void Update()
    {
        float songTime = Time.time - songStartTime; // Time elapsed since song started
        while (noteIndex < notes.Count && songTime >= notes[noteIndex].time)
        {
            SpawnKey(notes[noteIndex].noteNumber);
            noteIndex++;
        }
    }

    void ReadMidiFile()
{
    using (var stream = new System.IO.MemoryStream(midiFile.bytes))
    {
        var midi = MidiFile.Read(stream);
        var tempoMap = midi.GetTempoMap();
        var notesCollection = midi.GetNotes();

        foreach (var note in notesCollection)
        {
            double noteTimeInSeconds = TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap).TotalSeconds;
            notes.Add(new NoteData(note.NoteNumber, (float)noteTimeInSeconds));
        }

        notes = notes.OrderBy(n => n.time).ToList(); // Ensure they are in order
        Debug.Log("Loaded " + notes.Count + " notes from MIDI file.");
    }
}


    void SpawnKey(int noteNumber)
    {
        int laneIndex = GetLaneFromNoteNumber(noteNumber);
        if (laneIndex >= 0 && laneIndex < spawnPoints.Length)
        {
            Instantiate(keys[laneIndex], spawnPoints[laneIndex].position, Quaternion.identity);
            Debug.Log("Spawning note " + noteNumber + " in lane: " + laneIndex);
        }
    }

    int GetLaneFromNoteNumber(int noteNumber)
    {
        return noteNumber % keys.Length; // Maps MIDI note numbers to available lanes
    }
}

public class NoteData
{
    public int noteNumber;
    public float time;

    public NoteData(int noteNumber, float time)
    {
        this.noteNumber = noteNumber;
        this.time = time;
    }
}