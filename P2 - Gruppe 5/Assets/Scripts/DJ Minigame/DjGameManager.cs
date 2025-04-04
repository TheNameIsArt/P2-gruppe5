using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DjGameManager : MonoBehaviour
{

    public AudioSource music;
    public bool startPlaying;
    public TempoScroller tempoScroller;

    public static DjGameManager instance;
    public int currentScore;
    private int scorePerGoodNote = 100;
    private int scorePerGreatNote = 125;
    private int scorePerPerfectNote = 150;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
    }

    void Update()
    {
        
    }

    public void NoteHit()
    {
        Debug.Log("Hit on time");
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker ++;

        if(multiplierThresholds[currentMultiplier -1] <= multiplierTracker)
        {
            multiplierTracker = 0;
            currentMultiplier++;
        }

        multiplierText.text = "Multiplier: x" + currentMultiplier;

        //currentScore += scorePerGoodNote * currentMultiplier;
        scoreText.text = "Score: " + currentScore;
        }
        
    }

public void GoodHit()
{
    currentScore += scorePerGoodNote * currentMultiplier;
    NoteHit();
}

public void GreatHit()
{
    currentScore += scorePerGreatNote * currentMultiplier;
    NoteHit();
}

public void PerfectHit()
{
    currentScore += scorePerPerfectNote * currentMultiplier;
    NoteHit();
}

    public void NoteMissed()
    {
        Debug.Log("Missed note");

        currentMultiplier = 1;
        multiplierTracker = 0;
        multiplierText.text = "Multiplier: x" + currentMultiplier;
    }

    public void StartMusic()
{
    if (!music.isPlaying)
    {
        music.Play();
    }
}

}
