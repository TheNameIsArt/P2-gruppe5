using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ExpressionSelector : MonoBehaviour
{
    public Image antennaImage, eyesImage, mouthImage;
    public Sprite[] antennaSprites, eyesSprites, mouthSprites;

    private int antennaIndex = 0, eyesIndex = 0, mouthIndex = 0;

    public Button antennaLeft, antennaRight, eyesLeft, eyesRight, mouthLeft, mouthRight;

    public FaceGenerator faceGenerator;
    public TMP_Text pageNumberText, codeText;

    private string correctCampLetter;

    // Fixed code mappings to always appear on the same pages
    private readonly (string code, string letter, int page)[] codeMappings = new (string, string, int)[]
    {
        ("★◆●", "A", 1), ("☀☁✦", "B", 2), ("☉♠♣", "C", 3), ("ΩΦΨ", "D", 4), 
        ("△◉▣", "E", 5), ("♜♞♝", "F", 6), ("✿❀❁", "G", 7), ("⚡⚓☄", "H", 8)
    };

    void Start()
    {
        UpdateSprites();

        // Assign button listeners
        antennaLeft.onClick.AddListener(() => ChangeAntenna(-1));
        antennaRight.onClick.AddListener(() => ChangeAntenna(1));
        eyesLeft.onClick.AddListener(() => ChangeEyes(-1));
        eyesRight.onClick.AddListener(() => ChangeEyes(1));
        mouthLeft.onClick.AddListener(() => ChangeMouth(-1));
        mouthRight.onClick.AddListener(() => ChangeMouth(1));
    }

    void ChangeAntenna(int change)
    {
        antennaIndex = (antennaIndex + change + antennaSprites.Length) % antennaSprites.Length;
        UpdateSprites();
        CheckMatch();
    }

    void ChangeEyes(int change)
    {
        eyesIndex = (eyesIndex + change + eyesSprites.Length) % eyesSprites.Length;
        UpdateSprites();
        CheckMatch();
    }

    void ChangeMouth(int change)
    {
        mouthIndex = (mouthIndex + change + mouthSprites.Length) % mouthSprites.Length;
        UpdateSprites();
        CheckMatch();
    }

    void UpdateSprites()
    {
        antennaImage.sprite = antennaSprites[antennaIndex];
        eyesImage.sprite = eyesSprites[eyesIndex];
        mouthImage.sprite = mouthSprites[mouthIndex];
    }

    void CheckMatch()
    {
        if (faceGenerator.IsCorrectMatch(antennaIndex, eyesIndex, mouthIndex))
        {
            GenerateCodeAndPage();
        }
    }

    void GenerateCodeAndPage()
    {
        int randomIndex = Random.Range(0, codeMappings.Length);
        (string code, string letter, int page) selectedCode = codeMappings[randomIndex];

        pageNumberText.text = "Page: " + selectedCode.page;
        codeText.text = "Code: " + selectedCode.code;
        correctCampLetter = selectedCode.letter;

        // Send the correct letter to the Camp Area Manager
        Object.FindFirstObjectByType<CampAreaManager>().SetCorrectCampLetter(correctCampLetter);
    }
}
