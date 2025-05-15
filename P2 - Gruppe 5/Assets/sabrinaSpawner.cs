using UnityEngine;

public class sabrinaSpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!HatManager.Instance.sabrinaQuestStarter)
        {
            gameObject.SetActive(false);
        }
    }
}
