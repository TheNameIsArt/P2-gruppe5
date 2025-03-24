using UnityEngine;
using UnityEngine.UI;

public class FaceGenerator : MonoBehaviour
{
    // UI Images representing the face elements
    public Image antennaImage, eyesImage, mouthImage;
    
    // Arrays holding different sprite options for each facial feature
    public Sprite[] antennaSprites, eyesSprites, mouthSprites;

    // Stores the correct feature indices to match
    private int correctAntennaIndex, correctEyesIndex, correctMouthIndex;

    // Public properties to access the correct indices
    public int CorrectAntennaIndex => correctAntennaIndex;
    public int CorrectEyesIndex => correctEyesIndex;
    public int CorrectMouthIndex => correctMouthIndex;

    void Start()
    {
        GenerateNewFace(); // Generate and display a new face when the game starts
    }

    // Generates a new random face by selecting random sprite indices
    public void GenerateNewFace()
    {
        // Randomly pick an index for each feature
        correctAntennaIndex = Random.Range(0, antennaSprites.Length);
        correctEyesIndex = Random.Range(0, eyesSprites.Length);
        correctMouthIndex = Random.Range(0, mouthSprites.Length);

        // Assign the correct sprites to the face window
        antennaImage.sprite = antennaSprites[correctAntennaIndex];
        eyesImage.sprite = eyesSprites[correctEyesIndex];
        mouthImage.sprite = mouthSprites[correctMouthIndex];
    }

    // Checks if the provided facial feature indices match the correct ones
    public bool IsCorrectMatch(int antenna, int eyes, int mouth)
    {
        return (antenna == correctAntennaIndex && eyes == correctEyesIndex && mouth == correctMouthIndex);
    }
}
