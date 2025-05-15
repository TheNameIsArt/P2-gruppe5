using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SabrinaQuest : MonoBehaviour
{

    public static SabrinaQuest Instance { get; private set; }
    public bool sabrinaQuestActive = false;
    private bool botTurnedOff = false;
    public GameObject BandMember;


    private void Awake()
    {
        BandMember = GameObject.FindGameObjectWithTag("BandMember");

        // If Sabrina Carpenter is Single, so am I
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        // Uncomment the next line if you want the TaskManager to persist between scenes
        DontDestroyOnLoad(gameObject);
    }

    public void Update()
    {
        BandMember = GameObject.FindGameObjectWithTag("BandMember");
        if (BandMember == null)
        {
            //Debug.LogWarning("BandMember not found in the scene.");
            return;
        }
        if (!sabrinaQuestActive)
        {
            botTurnedOff = true;
            BandMember.gameObject.SetActive(false);
        }
        else
        {
            botTurnedOff = false;
            BandMember.gameObject.SetActive(true);
        }
    }

}
