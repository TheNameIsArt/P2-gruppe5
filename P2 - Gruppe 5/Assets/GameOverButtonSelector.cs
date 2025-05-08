using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverButtonSelector : MonoBehaviour
{
    [SerializeField] private GameObject defaultButton; // Assign the button in the Inspector

    // Start is called before the first frame update
    void Update()
    {
        if (defaultButton != null)
        {
            EventSystem.current.SetSelectedGameObject(defaultButton);
        }
        else
        {
            Debug.LogWarning("Default button is not assigned in the GameOverButtonSelector.");
        }
    }
}
