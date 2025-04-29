using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public SpriteRenderer winLight; // Assign the circle sprite in the Inspector
    [SerializeField] private YellowSpriteChecker yellowSpriteChecker; // Reference to the YellowSpriteChecker script

    void OnEnable()
    {
        LightBeam.WinEvent += OnWin;
    }

    void OnDisable()
    {
        LightBeam.WinEvent -= OnWin;
    }

    void OnWin()
    {
        Debug.Log("You win! Light reached the goal.");

            // Change the color from black to yellow
            if (winLight != null)
            {
                winLight.color = new Color (1f, 1f, 1f, 1f);
            }
            yellowSpriteChecker.light2 = true;
    }
}