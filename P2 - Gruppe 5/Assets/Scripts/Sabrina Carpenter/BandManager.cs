using UnityEngine;

public class BandManager : MonoBehaviour
{
    public GameObject BandMember;

    private void Start()
    {
        // Assigns the GameObject BandMember to gameobejcts tagged with BandMember
        BandMember = GameObject.FindGameObjectWithTag("BandMember");
    }

    private void Update()
    {
        SabrinaQuestline();
    }

    public void SabrinaQuestline()
    {
        // Makes the BandMember GameObject active in the map if the player has started the Sabrina Questline
        // Using the sabrinaQuestActive boolean from SabrinaQuest script to check active state
        if (SabrinaQuest.Instance.sabrinaQuestActive == true)
        {
            BandMember.gameObject.SetActive(true);
        }
        else
        {
            BandMember.gameObject.SetActive(false);
        }
    }
}
