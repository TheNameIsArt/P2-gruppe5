using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SabrinaQuest : MonoBehaviour
{

    public static SabrinaQuest Instance { get; private set; }
    public bool sabrinaQuestActive = false;

    private void Awake()
    {

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

}
