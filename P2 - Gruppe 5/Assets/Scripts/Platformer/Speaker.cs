using UnityEngine;

public class Speaker : MonoBehaviour
{
    public AudioSource audioSource;
    public float maxVolume = 1.0f;
    public float minVolume = 0.1f;
    public float maxDistance = 5.0f;
    private float volumeModifier = 1.0f; // Adjusted by headphones

    private Transform PlatformerPlayer;

    void Start()
    {
        PlatformerPlayer = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        float distance = Vector3.Distance(transform.position, PlatformerPlayer.position);
        float volume = Mathf.Lerp(maxVolume, minVolume, distance / maxDistance);
        audioSource.volume = Mathf.Clamp(volume * volumeModifier, 0f, maxVolume);
    }

    public void SetVolumeModifier(float modifier)
    {
        volumeModifier = modifier;
    }
}