using UnityEngine;

public class begone : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public static bool isBegone = false;
    void Start()
    {
        if (isBegone)
        {
            gameObject.SetActive(false);
            Debug.Log("Begone has been set to true.");
        }
    }

public void SetBegoneTrue()
    {
        isBegone = true;
        Debug.Log("Begone has been set to true from SetBegoneTrue method.");
    }
}
