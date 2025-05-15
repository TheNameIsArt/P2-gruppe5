using UnityEngine;

public class AndyQuestTracker : MonoBehaviour
{
    public static AndyQuestTracker Instance { get; private set; }
    public bool AndyTalkedAtCamp = false;
    private bool burgerTurnedOn = false;
    public GameObject BandMember;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        // singleton pattern
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    public void Update()
    {
        if (AndyTalkedAtCamp)
        {
            burgerTurnedOn = true;
            BandMember.gameObject.SetActive(false);
        }
    }
}
