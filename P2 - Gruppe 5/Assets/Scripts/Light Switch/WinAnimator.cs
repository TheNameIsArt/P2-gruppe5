using UnityEngine;

public class WinAnimator : MonoBehaviour
{

    private YellowSpriteChecker bigButtonManager;
    public Animator winAnimator;
    //public GameObject animationPlayer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winAnimator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (bigButtonManager.winConditionMet == true) 
        {
            winAnimator.Play("Victory");
        }
    }
}
