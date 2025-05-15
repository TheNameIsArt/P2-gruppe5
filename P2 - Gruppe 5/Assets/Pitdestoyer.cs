using UnityEngine;

public class Pitdestoyer : MonoBehaviour
{
    public static bool pitDestroyer = false;
    public GameObject newpit;
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
            newpit.SetActive(true);
        }
    }
    public void DestroyPit()
    {
        pitDestroyer = true;
        newpit.SetActive(true);
    }
}
