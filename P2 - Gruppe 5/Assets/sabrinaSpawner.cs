using UnityEngine;

public class sabrinaSpawner : MonoBehaviour
{
    private bool sabrinaQuestActive = false;
    [SerializeField] private GameObject Sabrina;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (!HatManager.Instance.sabrinaQuestStarter)
        {
            Sabrina.SetActive(false);
        }
    }
    void Update()
    {
        if (HatManager.Instance.sabrinaQuestStarter && !sabrinaQuestActive)
        {
            sabrinaQuestActive = true;
            Sabrina.SetActive(true);
        }
    }
}
