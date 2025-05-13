using DialogueEditor;
using UnityEngine;

public class CrowdByeBye : MonoBehaviour
{
    private static bool burgerGameStarted = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (burgerGameStarted)
        {
            gameObject.SetActive(false);
        }
    }
    public void StartBurgerGame()
    {
        burgerGameStarted = true;
        
    }
}
