using UnityEngine;

public class cutscenestarter : MonoBehaviour
{
    public Level_Changer levelChanger;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartCutscene()
    {
        levelChanger.sceneChanger();
    }
}
