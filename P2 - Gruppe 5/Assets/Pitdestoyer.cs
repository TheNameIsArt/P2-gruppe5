using UnityEngine;

public class Pitdestoyer : MonoBehaviour
{
    public static bool pitDestroyer = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (pitDestroyer)
        {
            Destroy(gameObject);
        }
    }
    public void DestroyPit()
    {
        pitDestroyer = true;
    }
}
