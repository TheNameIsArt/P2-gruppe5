using System.Collections.Generic;
using UnityEngine;

public class ProjectilePooler : MonoBehaviour
{
    public GameObject projectilePrefab; // The projectile to pool
    public int poolSize = 20;           // Initial number of projectiles in the pool

    private List<GameObject> pool;

    void Awake()
    {
        pool = new List<GameObject>();

        // Create a pool of inactive projectiles at the start
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(projectilePrefab);
            obj.SetActive(false); // Disable until needed
            pool.Add(obj);
        }
    }

    /// Returns an available projectile from the pool or expands the pool if needed.
    public GameObject GetPooledProjectile()
    {
        foreach (GameObject obj in pool)
        {
            if (!obj.activeInHierarchy)
                return obj;
        }

        // Optional: expand pool dynamically if all are in use
        GameObject newObj = Instantiate(projectilePrefab);
        newObj.SetActive(false);
        pool.Add(newObj);
        return newObj;
    }
}

