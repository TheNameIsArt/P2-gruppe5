using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_HatSwitcher : MonoBehaviour
{
    public string currentHat;
    public string [] hats;
    public Animator animator;
    public AnimationClip [] hatsIdle;
    public AnimationClip[] hatsRun;

    public AnimationClip idle;
    public AnimationClip run;

   

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHat = hats[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (currentHat == "Burger")
        {
            idle = hatsIdle[0];
            run = hatsRun[0];
        }
        else if (currentHat == "Construction")
        {
            idle = hatsIdle[1];
            run = hatsRun[1];
        }
        else if (currentHat == "Lightswitch")
        {
            idle = hatsIdle[2];
            run = hatsRun[2];
        }
    }

    public void animateIdle()
    {
        animator.Play(idle.name);
    }
    public void animateRun() 
    {
        animator.Play(run.name);
    }
    public void nextHat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            
            Debug.Log("Next Hat!");
        }
    }
    public void previousHat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            //currentHat -= 1;
        }
    }
}

