using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using DialogueEditor;
using UnityEngine.InputSystem;

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

    private int currentMultiplier;
    private int multiplierTracker;
    public int[] multiplierThresholds;

    public GameObject gameoverUI;
    private float musicDelay = 2.9f;

    private bool isGameStarted = false; // Track if the game has started
    private bool gameOver = false; // Track if the game is over

    [SerializeField] private NPCConversation myConversation;

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
        Invoke ("StartMusic", musicDelay);
    }

    void Update()
    {
        if (isGameStarted && !music.isPlaying && !gameOver)
        {
            Win();
        }
    }

   public void NoteHit()
{
    Debug.Log("Hit on time");

    /* if (currentMisses > 0)
    {
        currentMisses --;
        missCounterText.text = "Misses:" + currentMisses + "/10";
    } */

    // Handle multiplier progression only if within range
    if (currentMultiplier - 1 < multiplierThresholds.Length)
    {
        multiplierTracker++;

        if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
        {
            multiplierTracker = 0;
            currentMultiplier++;
        }
    }

    // Always update the multiplier and score display
    multiplierText.text = "Multiplier: x" + currentMultiplier;
    scoreText.text = "Score: " + currentScore;
}


    public void StartMusic()
    {
        if (!music.isPlaying && !isGameStarted)
        {
            music.Play();
            isGameStarted = true;
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
       // missCounterText.text = "Misses: " + currentMisses + "/10";

        /* if (currentMisses >= maxMisses)
         {
             GameOver();
         } */

    }

    /* void GameOver()
     {
         Debug.Log("Game Over!");
         Time.timeScale = 0f; //Game gets paused
         gameoverUI.SetActive(true);
         music.Stop();
         gameOver = true;
     }

     public void TryAgainController(InputAction.CallbackContext context)
     {
         if (context.performed)
         {
             Time.timeScale = 1f;
             SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
         }
     } */

    /* public void TryAgain()
    {
            Time.timeScale = 1f;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    } */

    public void Win()
    {
        Debug.Log("You win!");
        gameOver = true;
        if (myConversation != null)
        {
            ConversationManager.Instance.StartConversation(myConversation);
        }
        else
        {
            Debug.LogWarning("No conversation assigned to the ConversationEditer.");
        }
    }
}



