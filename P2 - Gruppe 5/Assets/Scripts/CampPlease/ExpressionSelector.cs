using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpressionSelector : MonoBehaviour
{
    // UI elements for the different expression items
    public Image antennaImage, eyesImage, mouthImage;
    public Sprite[] antennaSprites, eyesSprites, mouthSprites; // Making an array to store different sprites to choose from

    // Indexes that track the current selections
    private int antennaIndex = 0, eyesIndex = 0, mouthIndex = 0;

    // UI buttons to navigate through the sprite options
    public Button antennaLeft, antennaRight, eyesLeft, eyesRight, mouthLeft, mouthRight;

    // Reference to the FaceGenerator script, that checks if the chosen items match the face
    public FaceGenerator faceGenerator;

    // UI elements for displaying generated codes and page numbers
    public TMP_Text pageNumberText, codeText;

    // Stores the correct camp letter to be sent to CampAreaManager script
    private string correctCampLetter;

    // Predefined set of codes, letters, and page numbers
    private readonly (string code, string letter, int page)[] codeMappings = new (string, string, int)[]
    {
        ("@ * ! * ?", "A", 1), ("# $ & + *", "B", 2), ("? ? # ¤ $", "C", 3), ("¤ ¤ @ @ !", "D", 4), 
        ("+ / & £ >", "E", 5), ("< * # $ &", "F", 6), ("% % ¤ # £", "G", 7), ("£ ! @ % +", "H", 8)
    };

    void Start()
    {
        UpdateSprites(); // Initialize the UI with default sprites

        // Assign button listeners
        antennaLeft.onClick.AddListener(() => ChangeAntenna(-1));
        antennaRight.onClick.AddListener(() => ChangeAntenna(1));
        eyesLeft.onClick.AddListener(() => ChangeEyes(-1));
        eyesRight.onClick.AddListener(() => ChangeEyes(1));
        mouthLeft.onClick.AddListener(() => ChangeMouth(-1));
        mouthRight.onClick.AddListener(() => ChangeMouth(1));
    }

    // Changes the selected Antenna sprite
    void ChangeAntenna(int change)
    {
        antennaIndex = (antennaIndex + change + antennaSprites.Length) % antennaSprites.Length;
        UpdateSprites();
        CheckMatch();
    }

    // Changes the selected Eyes sprite
    void ChangeEyes(int change)
    {
        eyesIndex = (eyesIndex + change + eyesSprites.Length) % eyesSprites.Length;
        UpdateSprites();
        CheckMatch();
    }

    // Changes the selected Mouth sprite
    void ChangeMouth(int change)
    {
        mouthIndex = (mouthIndex + change + mouthSprites.Length) % mouthSprites.Length;
        UpdateSprites();
        CheckMatch();
    }

    // Updates the displayed sprites based on the current indexes
    void UpdateSprites()
    {
        antennaImage.sprite = antennaSprites[antennaIndex];
        eyesImage.sprite = eyesSprites[eyesIndex];
        mouthImage.sprite = mouthSprites[mouthIndex];
    }

    // Checks if the selected combination matches a predefined correct pattern
    void CheckMatch()
    {
        if (faceGenerator.IsCorrectMatch(antennaIndex, eyesIndex, mouthIndex))
        {
            GenerateCodeAndPage(); // Generate and display the corresponding code and page
        }
    }

    // Generates a random code and page number from the predefined mappings
    void GenerateCodeAndPage()
    {
        int randomIndex = Random.Range(0, codeMappings.Length);
        (string code, string letter, int page) selectedCode = codeMappings[randomIndex];

        // Update UI elements with the generated values
        pageNumberText.text = "Page: " + selectedCode.page;
        codeText.text = "Code: " + selectedCode.code;
        correctCampLetter = selectedCode.letter;

        // Send the correct letter to the CampAreaManager script
        Object.FindFirstObjectByType<CampAreaManager>().SetCorrectCampLetter(correctCampLetter);
    }
}
