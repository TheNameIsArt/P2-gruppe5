using UnityEngine;

public class CampDialogue : MonoBehaviour
{
    public void SpawnCampBots()
    {
        CampingSpawner.Instance.SpawnCampBots = true;
    }
}
