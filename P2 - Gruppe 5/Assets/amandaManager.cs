using Unity.VisualScripting;
using UnityEngine;

public class amandaManager : MonoBehaviour
{
    public static bool amandaHappy = false;
    public GameObject amanda;
    public GameObject amandaHappyObject;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (amandaHappy)
        {
            Debug.Log("Amanda is happy!");
            Destroy(amanda);
            amandaHappyObject.SetActive(true);
        }
        else
        {
            Debug.Log("Amanda is not happy.");
            amanda.SetActive(true);
            amandaHappyObject.SetActive(false);
        }
    }

    // Update is called once per frame
    public void MakeAmandaHappy()
    {
        amandaHappy = true;
        Destroy(amanda);
        amandaHappyObject.SetActive(true);
    }


}
