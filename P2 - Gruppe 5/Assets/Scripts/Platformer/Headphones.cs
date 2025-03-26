using UnityEngine;

public class Headphones : MonoBehaviour
{
    public float volumeReduction = 0.3f; // Adjust this for effect strength


    private void Update()
    {
       
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Speaker[] speakers = FindObjectsOfType<Speaker>();

            foreach (Speaker speaker in speakers)
            {
                speaker.SetVolumeModifier(volumeReduction);
            }

            Destroy(gameObject);
        }
    }
}