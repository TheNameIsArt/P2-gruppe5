using UnityEngine;
using TMPro;

public class PickManager : MonoBehaviour
{
    public ExpressionBoxes[] expressionBoxes; // Array to hold references to boxes
    
    // Reference to the sprite renderers for the 3 boxes
    public SpriteRenderer antennaBox; 
    public SpriteRenderer eyesBox;
    public SpriteRenderer mouthBox;

    private int currentBox = 0; // Tracks current selected box
    private bool gameEnded = false; // Checks if game has ended

    // UI text elements
    public TMP_Text text;
    public TMP_Text pageNumberText;
    public TMP_Text codeText;

    void Start()
    {
        
    }

    void Update()
    {
        if (!gameEnded)
        {
            // Navigate through the spinning boxes using WASD keys, controller R1/L1, or arrow keys
            if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetButtonDown("R1"))
            {
                ChangeSelection(1); // Move to the next box
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetButtonDown("L1"))
            {
                ChangeSelection(-1); // Move to the previous box
            }
            
            // Confirm selection using mouse click or controller A/X button
            else if (Input.GetMouseButtonDown(0) || Input.GetButtonDown("Submit"))
            {
                ConfirmSelection(); // Confirm selection for the current box
            }
        }
    }
}
