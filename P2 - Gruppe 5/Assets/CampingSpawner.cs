using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CampingSpawner : MonoBehaviour
{

    public static CampingSpawner Instance { get; private set; }
    public bool SpawnCampBots = false;
    private bool botTurnedOff = false;

    public bool talkedWithCampBots = false;
    public GameObject campBots;
    public GameObject campBots2;


    private void Awake()
    {
        campBots = GameObject.FindGameObjectWithTag("campBots");
        campBots2 = GameObject.FindGameObjectWithTag("campBots2");

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
        campBots = GameObject.FindGameObjectWithTag("campBots");

        if (!SpawnCampBots)
        {
            botTurnedOff = true;
            campBots.gameObject.SetActive(false);
        }
        else
        {
            botTurnedOff = false;
            campBots.gameObject.SetActive(true);
        }
        if (talkedWithCampBots)
        {
            campBots2.gameObject.SetActive(false);
        }
        else
        {
            campBots2.gameObject.SetActive(true);
        }
    }

    public void SwitchCampBots()
    {
        if (campBots != null)
            campBots.SetActive(false);
        if (campBots2 != null)
            campBots2.SetActive(true);
    }

}
