using UnityEngine;

public class Zafrondestroyer : MonoBehaviour
{
    public static bool zafronDestroyer = false;
    public GameObject zafron;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (zafronDestroyer)
        {
            Destroy(zafron);
            
        }
    }
    public void DestroyPit()
    {
        zafronDestroyer = true;  
    }
}
