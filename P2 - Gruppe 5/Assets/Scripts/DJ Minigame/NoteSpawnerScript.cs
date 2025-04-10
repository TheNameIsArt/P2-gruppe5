using UnityEngine;
using Melanchall.DryWetMidi.Core;
using Melanchall.DryWetMidi.Interaction;
using System.Collections.Generic;
using System.IO;

public class NoteSpawnerScript : MonoBehaviour
{
    public GameObject[] keys; // Different note prefabs for each lane
    public Transform[] spawnPoints; // Spawn positions per lane
    public TextAsset midiFile; // MIDI file to read from (drag & drop in the Inspector)

    private float songStartTime;
    private List<MidiNoteData> noteEvents = new List<MidiNoteData>();
    private int nextNoteIndex = 0;

    void Start()
    {
        songStartTime = Time.time;
        LoadMidiNotes();
    }

    void Update()
    {
        float songPosition = Time.time - songStartTime; // Time since song started

        while (nextNoteIndex < noteEvents.Count && songPosition >= noteEvents[nextNoteIndex].time)
        {
            SpawnKey(noteEvents[nextNoteIndex]);
            nextNoteIndex++;
        }
    }

    void LoadMidiNotes()
    {
        if (midiFile == null)
        {
            Debug.LogError("MIDI file not assigned!");
            return;
        }

        byte[] midiBytes = midiFile.bytes;
        MemoryStream stream = new MemoryStream(midiBytes);
        MidiFile midi = MidiFile.Read(stream);

        TempoMap tempoMap = midi.GetTempoMap();

        foreach (var note in midi.GetNotes())
        {
            float timeInSeconds = (float)TimeConverter.ConvertTo<MetricTimeSpan>(note.Time, tempoMap).TotalSeconds;
            int lane = GetLaneFromMidiNote(note.NoteNumber);

            if (lane >= 0 && lane < spawnPoints.Length)
            {
                noteEvents.Add(new MidiNoteData { time = timeInSeconds, lane = lane });
            }
        }

        noteEvents.Sort((a, b) => a.time.CompareTo(b.time)); // Ensure events are in order
    }

    void SpawnKey(MidiNoteData noteData)
    {
        Instantiate(keys[noteData.lane], spawnPoints[noteData.lane].position, Quaternion.identity);
        Debug.Log($"Spawning note at {noteData.time} seconds in lane {noteData.lane}");
    }

    int GetLaneFromMidiNote(int midiNote)
    {
        // Example: Map MIDI notes to lanes (adjust based on your game)
        if (midiNote >= 60 && midiNote <= 63) return midiNote - 60; // Maps C4-F4 to lanes 0-3
        return -1; // Ignore notes outside the mapped range
    }

    private struct MidiNoteData
    {
        public float time;
        public int lane;
    }
}
