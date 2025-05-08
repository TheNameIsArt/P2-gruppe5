using UnityEngine;

public class PlateSpawner : MonoBehaviour
{
    public PlatePooler platePooler;
    public Transform[] spawnPoints;
    public float spawnInterval = 3f;

    void Start()
    {
        InvokeRepeating(nameof(SpawnPlate), 1f, spawnInterval);
    }

    void SpawnPlate()
    {
        GameObject plate = platePooler.GetPlate();
        plate.transform.position = spawnPoints[Random.Range(0, spawnPoints.Length)].position;

        var smartScript = plate.GetComponent<SmartDodgingPlate>();
        if (smartScript != null)
        {
            smartScript.pooler = platePooler;
        }
    }
}


