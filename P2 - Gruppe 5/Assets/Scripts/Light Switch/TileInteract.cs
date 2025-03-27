using UnityEngine;
using UnityEngine.UI;

public class TileInteraction : MonoBehaviour
{
    public bool isMirror = false;  // Set if the tile is a mirror
    public string controllerButton = "Fire1";  // The controller button input (can be mapped in Input settings)

    public Button myButton;  // Reference to the Button in the Inspector

    void Start()
    {
        myButton = GetComponent<Button>();
        // If the button is assigned in the Inspector, add an onClick listener
        if (myButton != null)
        {
            myButton.onClick.AddListener(OnButtonClick);  // Link the button click event to OnButtonClick()
        }
    }

    void OnButtonClick()
    {
        Debug.Log("Button clicked!");
        if (isMirror)
        {
            RotateMirror();
        }
    }

    void RotateMirror()
    {
        // Rotate the tile (mirror) by 90 degrees
        transform.Rotate(0f, 0f, 45f);
    }
}