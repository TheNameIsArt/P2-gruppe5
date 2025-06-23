using Cinemachine;
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
        // Try to set currentHatIndex from HatManager
        if (HatManager.Instance != null && !string.IsNullOrEmpty(HatManager.Instance.SavedHat))
        {
            int idx = System.Array.IndexOf(hats, HatManager.Instance.SavedHat);
            if (idx >= 0)
            {
                currentHatIndex = idx;
            }
        }

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
        else if (currentHat == "Sabrina")
        {
            idle = hatsIdle[4];
            run = hatsRun[4];
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
            // Save the new hat
            if (HatManager.Instance != null)
                HatManager.Instance.SavedHat = hats[currentHatIndex];
        }
    }
    public void previousHat(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            if (--currentHatIndex < 0)
            {
                currentHatIndex = hats.Length -1;
            }
            // Save the new hat
            if (HatManager.Instance != null)
                HatManager.Instance.SavedHat = hats[currentHatIndex];
        }
    }
}

