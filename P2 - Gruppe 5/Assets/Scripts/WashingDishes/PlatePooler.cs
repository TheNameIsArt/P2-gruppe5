using System.Collections.Generic;
using UnityEngine;

public class PlatePooler : MonoBehaviour
{
    public GameObject platePrefab;
    public int poolSize = 10;
    private Queue<GameObject> pool = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject plate = Instantiate(platePrefab);
            plate.SetActive(false);
            pool.Enqueue(plate);
        }
    }

    public GameObject GetPlate()
    {
        if (pool.Count > 0)
        {
            GameObject plate = pool.Dequeue();
            plate.SetActive(true);
            return plate;
        }

        // Optional: expand pool
        GameObject newPlate = Instantiate(platePrefab);
        return newPlate;
    }

    public void ReturnPlate(GameObject plate)
    {
        plate.SetActive(false);
        pool.Enqueue(plate);
    }
}
