using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Tasklist : MonoBehaviour
{

    public int Selector;
    public int newSelector;
    public string[] Commands = { "OPPONENT EYES!", "OPPONENT CARDS!", "OWN CARDS!"};
    [SerializeField] private float Timer;
    private bool timeLeft;
    public TMP_Text lookerSelection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        Selector = Random.Range(0, Commands.Length-1);
        timeLeft = true;
    }

    // Update is called once per frame
    void Update()
    {
        lookerSelection.text = Commands[Selector];

        if (Timer <= 0)
        {
            timeLeft = false;
            lookerSelection.text = "NO MORE TIME!!";
        }
    }

    public void Tasks()
    {
        // Run a new Random.Range to try and brute-force another integer chosen for the Commands array
        newSelector = Random.Range(0, Commands.Length-1);

        if (newSelector == Selector)
        {
            newSelector = Random.Range(0, Commands.Length - 1);
            Selector = newSelector;
        }
        else
        {
            Selector = newSelector;
        }
         lookerSelection.text = Commands[newSelector];
    }

    public void timeLimit ()
    {
        Timer = Random.Range(2f, 5f);
        Timer -= Time.deltaTime;        
    }

}
