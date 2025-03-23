using UnityEngine;
using UnityEngine.UI;

public class FaceGenerator : MonoBehaviour
{
    public Image antennaImage, eyesImage, mouthImage; // UI Images for robot face
    public Sprite[] antennaSprites, eyesSprites, mouthSprites; // Possible sprite options

    private int correctAntennaIndex, correctEyesIndex, correctMouthIndex;

    public int CorrectAntennaIndex => correctAntennaIndex;
    public int CorrectEyesIndex => correctEyesIndex;
    public int CorrectMouthIndex => correctMouthIndex;

    void Start()
    {
        GenerateNewFace(); // Generate and display the robot face on start
    }

    public void GenerateNewFace()
    {
        // Randomly pick an index for each feature
        correctAntennaIndex = Random.Range(0, antennaSprites.Length);
        correctEyesIndex = Random.Range(0, eyesSprites.Length);
        correctMouthIndex = Random.Range(0, mouthSprites.Length);

        // Assign the correct sprites to the robot face window
        antennaImage.sprite = antennaSprites[correctAntennaIndex];
        eyesImage.sprite = eyesSprites[correctEyesIndex];
        mouthImage.sprite = mouthSprites[correctMouthIndex];
    }

    public bool IsCorrectMatch(int antenna, int eyes, int mouth)
    {
        return (antenna == correctAntennaIndex && eyes == correctEyesIndex && mouth == correctMouthIndex);
    }
}
