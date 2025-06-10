using UnityEngine;
using UnityEngine.EventSystems;

public class confimButton : MonoBehaviour, ISelectHandler
{
    [SerializeField] private GameObject backButton; // Reference to the back button GameObject

    public void OnSelect(BaseEventData eventData)
    {
        if (backButton != null)
        {
            backButton.SetActive(false);
        }
    }
}
