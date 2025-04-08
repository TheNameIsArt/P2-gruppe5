using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class DjGameManager : MonoBehaviour
{

    public AudioSource music;
    public bool startPlaying;
    public TempoScroller tempoScroller;

    public static DjGameManager instance;
    public bool HasStartedMusic { get; set; } = false; // Track if the music has started
    public int currentScore;

    private int maxMisses = 10; //Amount if misses allowed
    private int currentMisses = 0;

    private int scorePerGoodNote = 100;
    private int scorePerGreatNote = 125;
    private int scorePerPerfectNote = 150;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI multiplierText;
    public TextMeshProUGUI missCounterText;

    public int currentMultiplier;
    public int multiplierTracker;
    public int[] multiplierThresholds;

    public GameObject gameoverUI;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

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

    public void StartMusic()
    {
        if (!music.isPlaying)
        {
            music.Play();
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
        currentMisses++;

        currentMultiplier = 1;
        multiplierTracker = 0;
        multiplierText.text = "Multiplier: x" + currentMultiplier;
        missCounterText.text = "Misses: " + currentMisses;

        if (currentMisses >= maxMisses)
        {
            GameOver();
        }

    }

    void GameOver()
    {
        Debug.Log("Game Over!");
        Time.timeScale = 0f; //Game gets paused
        gameoverUI.SetActive(true);
        music.Stop();
    }

    public void TryAgain()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}



