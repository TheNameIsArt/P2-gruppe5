using Cinemachine;
using UnityEditor.Rendering;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_HatSwitcher : MonoBehaviour
{
    public string currentHat;
    public string [] hats;
    public Animator animator;

    public GameObject hatObjectIdle;
    public GameObject hatObjectRun;

    public SpriteRenderer[] hatsIdle;
    public SpriteRenderer[] hatsRun;

    private SpriteRenderer idle;
    private SpriteRenderer run;

    public int currentHatIndex = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        idle = hatsIdle[0];
        run = hatsRun[0];

        hatObjectIdle.SetActive(true);
        hatObjectRun.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        currentHat = hats[currentHatIndex];
        if (currentHat == "NoHat")
        {
            idle = hatsIdle[0];
            run = hatsRun[0];

        }
        currentHat = hats[currentHatIndex];
        if (currentHat == "Burger")
        {
            idle = hatsIdle[1];
            run = hatsRun[1];

        }
        else if (currentHat == "Construction")
        {
            idle = hatsIdle[2];
            run = hatsRun[2];

        }
        else if (currentHat == "Lightswitch")
        {
            idle = hatsIdle[3];
            run = hatsRun[3];

        }

        idle.color = new Color(1f, 1f, 1f, 1f);
        run.color = new Color(1f, 1f, 1f, 1f);

        foreach (SpriteRenderer hats in hatsIdle)
        {
            if ( hats != idle)
            {
                hats.color = new Color(1f, 1f, 1f, 0f); ;
            }
        }
        foreach (SpriteRenderer hats in hatsRun)
        {
            if (hats != run)
            {
                hats.color = new Color(1f, 1f, 1f, 0f); ;
            }
        }
    }

    public void HatIdle()
    {
        //animator.Play(idle.name);
        hatObjectIdle.SetActive(true);
        hatObjectRun.SetActive(false);

    }
    public void HatRun() 
    {
        //animator.Play(run.name);
        hatObjectRun.SetActive(true);
        hatObjectIdle.SetActive(false);

    }
    public void nextHat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (++currentHatIndex == hats.Length)
            {
                currentHatIndex = 0;
            }
        }
    }
    public void previousHat(InputAction.CallbackContext context)
    {
     
    }
}

