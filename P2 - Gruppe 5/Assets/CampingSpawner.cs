using UnityEngine;

public class CampingSpawner : MonoBehaviour
{
    public static CampingSpawner Instance { get; private set; }

    public bool SpawnCampBots = false;
    public bool talkedWithCampBots = false;

    public GameObject[] campBots;
    public GameObject[] campBots2;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        // Always re-find bots in case we changed scenes
        campBots = GameObject.FindGameObjectsWithTag("campBots");
        campBots2 = GameObject.FindGameObjectsWithTag("campBots2");

        UpdateCampBotsState();
    }

    private void UpdateCampBotsState()
    {
        if (talkedWithCampBots)
        {
            SpawnCampBots = false;

            foreach (GameObject bot in campBots)
            {
                if (bot != null && bot.activeSelf)
                    bot.SetActive(false);
            }

            foreach (GameObject bot in campBots2)
            {
                if (bot != null && !bot.activeSelf)
                    bot.SetActive(true);
            }
        }
        else
        {
            foreach (GameObject bot in campBots)
            {
                if (bot != null)
                    bot.SetActive(SpawnCampBots);
            }

            foreach (GameObject bot in campBots2)
            {
                if (bot != null && bot.activeSelf)
                    bot.SetActive(false);
            }
        }
    }
}