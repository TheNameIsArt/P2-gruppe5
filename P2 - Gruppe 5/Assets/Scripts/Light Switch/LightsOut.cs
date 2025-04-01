using UnityEngine;
using UnityEngine.UI;

public class LightsOut : MonoBehaviour
{
    public SpriteRenderer[] lightButtons; // Assign these in the Inspector
    private bool[] lightStates;   // Store the On/Off state of lights
    public SpriteRenderer winLight;
    private AudioSource buttonSound;
    void Start()
    {
        buttonSound = GetComponent<AudioSource>();
        lightStates = new bool[lightButtons.Length];
        InitializeLights();
    }

    void InitializeLights()
    {
        // Example: Alternate Pattern
        bool[] startPattern = { true, false, true, false, true };

        for (int i = 0; i < lightStates.Length; i++)
        {
            lightStates[i] = startPattern[i];
            UpdateLightAppearance(i);
        }
    }


    public void OnLightClicked(int index)
    {
        ToggleLight(index); // Toggle clicked light

        // Toggle neighboring lights
        if (index > 0) ToggleLight(index - 1);
        if (index < lightStates.Length - 1) ToggleLight(index + 1);

        // Wraparound logic: last affects first, first affects last
        if (index == 0) ToggleLight(lightStates.Length - 1);
        if (index == lightStates.Length - 1) ToggleLight(0);

        buttonSound.Play();
        CheckWinCondition();
    }


    void ToggleLight(int index)
    {
        lightStates[index] = !lightStates[index];
        UpdateLightAppearance(index);
    }

    void UpdateLightAppearance(int index)
    {
        lightButtons[index].color = lightStates[index] ? Color.yellow : Color.black;
    }

    void CheckWinCondition()
    {
        foreach (bool state in lightStates)
        {
            if (!state) return; // If any light is still off, game isn't won
        }

        Debug.Log("You win! All lights are ON!");
        OnWin();
    }
    void OnWin()
    {

        // Change the color from black to yellow
        if (winLight != null)
        {
            winLight.color = Color.yellow;
        }
    }
}