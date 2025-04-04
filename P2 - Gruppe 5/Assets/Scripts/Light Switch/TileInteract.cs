using UnityEngine;
using UnityEngine.UI;

public class TileInteraction : MonoBehaviour
{
    public bool isMirror = false;  // Set if the tile is a mirror
    public string controllerButton = "Fire1";  // The controller button input (can be mapped in Input settings)
    [SerializeField] private GameObject parent;

    public Button myButton;  // Reference to the Button in the Inspector
    [SerializeField] private AudioSource buttonSound;  // Reference to the AudioSource in the Inspector

    void Start()
    {
        parent = transform.parent.gameObject;
        myButton = GetComponent<Button>();
        buttonSound = GetComponent<AudioSource>();
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
            buttonSound.Play();
        }
    }

    void RotateMirror()
    {
        // Rotate the tile (mirror) by 45 degrees
        parent.transform.Rotate(0f, 0f, 45f);
    }
}