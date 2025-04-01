using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DjGameManager : MonoBehaviour
{
    public static DjGameManager instance;
    public AudioSource music;
    public bool IsMusicPlaying { get; private set; }
    public TempoScroller musicTempo;
 

    private int currentScore; // Score shown in the corner
    private int scorePerNote = 50; // Score given for each hit 
    private int scorePerGoodNote = 100;
    private int scorePerPerfectNote = 150;
    private int currentMultiplier; // Current multiplier shown in the corner
    private int multiplierTracker; // Tracks the multiplier
    public int [] multiplierThresholds; // Array of multipliers. Once the consecutive notes are hit, the multiplier moves on to the next one. 
    public TextMeshProUGUI scoreText; // Reference to the TMPro for scoretext
    public TextMeshProUGUI multiplierText; // Reference to the TMPro for multipliertext
    void Start()
    {
        instance = this;
        scoreText.text = "Score: 0";
        currentMultiplier = 1;
        multiplierText.text = "Multiplier x1";
    }

    void Update()
    {
        
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void StartMusic()
    {
        if (!IsMusicPlaying)
        {
            music.Play();
            IsMusicPlaying = true;
        }
    }

    public void NoteHit()
    {
        //Debug.Log("Hit on time");
        if(currentMultiplier - 1 < multiplierThresholds.Length)
        {
            multiplierTracker ++;

            if(multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier ++;
            }
        }

        multiplierText.text = "Multiplier: x" + currentMultiplier;

      

        //currentScore += scorePerNote * currentMultiplier;
        scoreText.text = "Score:" + currentScore;
    }

    public void NormalHit()
    {
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    public void GoodHit()
    {
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }

    public void PerfectHit()
    {
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }

    public void NoteMissed()
    {
        //Debug.Log("Missed note");

        currentMultiplier = 1;
        multiplierTracker = 0;

        multiplierText.text = "Multiplier: x" + currentMultiplier;
    }
}
