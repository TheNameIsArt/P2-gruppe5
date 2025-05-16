using UnityEngine;

public class CampDialogue2 : MonoBehaviour
{
    public void SpawnCampBots2()
    {
        CampingSpawner.Instance.talkedWithCampBots = true;
        CampingSpawner.Instance.SpawnCampBots = false;
    }
}