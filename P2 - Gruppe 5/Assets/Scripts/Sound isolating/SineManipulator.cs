using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem; // For the new Input System (optional)

public class SineManipulator : MonoBehaviour
{
    public Sinewave controledSinewave;
    [SerializeField] private float currentAmplitude;
    [SerializeField] private float currentFrequenzy;
    private float amplitudeChanger = 0.01f;
    private float frequenzyChanger = 0.01f;
    public Sinewave targetSinewave;
    public Slider progressBar;
    public GameObject scannerUI;
    public bool Won = false;
    public AudioSource currentAudio;
    public InputDeviceSwitcher inputDeviceSwitcher;

    private void Awake()
    {
        progressBar = GameObject.Find("Progress Bar").GetComponent<Slider>();
        progressBar.value = 0;
        controledSinewave = GetComponent<Sinewave>();
        scannerUI = GameObject.Find("Scanner UI");
        targetSinewave = GameObject.Find("Sinewave").GetComponent<Sinewave>();
        

        float randomValue1 = Random.Range(0.0f, 1.0f);
        float randomValue2 = Random.Range(0.0f, 1.0f);
        Debug.Log("Random Floats: " + randomValue1 + " and " + randomValue2);

        controledSinewave.frequency = randomValue1;
        controledSinewave.amplitude = randomValue2;
        inputDeviceSwitcher = GameObject.Find("Cursor Canvas").GetComponent<InputDeviceSwitcher>();
        if (inputDeviceSwitcher == null)
        {
            Debug.LogError("InputDeviceSwitcher not found in the scene.");
        }
        inputDeviceSwitcher.MouseOff(); // Reset the game over state

    }


    void FixedUpdate()
    {
        // **PC Controls**
        float mouseX = Input.GetAxis("Mouse X");    // Mouse movement (X) controls frequency
        bool keyIncreaseAmplitude = Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow);
        bool keyDecreaseAmplitude = Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow);
        bool keyIncreaseFrequency = Input.GetKey(KeyCode.RightArrow);
        bool keyDecreaseFrequency = Input.GetKey(KeyCode.LeftArrow);

        // **Controller Controls**
        float leftStickX = Gamepad.current?.leftStick.x.ReadValue() ?? 0f;
        float rightStickY = Gamepad.current?.rightStick.y.ReadValue() ?? 0f;

        // **Adjust Frequency (Mouse, Arrows, Controller Left Stick)**
        if (mouseX < 0 || keyDecreaseFrequency || leftStickX < -0.1f)
        {
            currentFrequenzy = controledSinewave.frequency + frequenzyChanger;
            currentFrequenzy = Mathf.Clamp(currentFrequenzy, 0f, 1.5f);
            controledSinewave.frequency = currentFrequenzy;
        }
        else if (mouseX > 0 || keyIncreaseFrequency || leftStickX > 0.1f)
        {
            currentFrequenzy = controledSinewave.frequency - frequenzyChanger;
            currentFrequenzy = Mathf.Clamp(currentFrequenzy, 0f, 1.5f);
            controledSinewave.frequency = currentFrequenzy;
        }

        // **Adjust Amplitude (W/S Keys, Right Stick)**
        if (keyIncreaseAmplitude || rightStickY > 0.1f)
        {
            currentAmplitude = controledSinewave.amplitude + amplitudeChanger;
            currentAmplitude = Mathf.Clamp(currentAmplitude, 0f, 1f);
            controledSinewave.amplitude = currentAmplitude;
        }
        else if (keyDecreaseAmplitude || rightStickY < -0.1f)
        {
            currentAmplitude = controledSinewave.amplitude - amplitudeChanger;
            currentAmplitude = Mathf.Clamp(currentAmplitude, 0f, 1f);
            controledSinewave.amplitude = currentAmplitude;
        }
        // Check if the Sinewaves Match
        if (Mathf.Abs(targetSinewave.amplitude - currentAmplitude) <= 0.05f &&
            Mathf.Abs(targetSinewave.frequency - currentFrequenzy) <= 0.01f)
        {
            ProgressBarFilling();
        }
        else if (progressBar.enabled)
        {
            ProgressBarUnfilling();
        }
    }
    private void ProgressBarFilling()
    {
        if (!progressBar.enabled)
        {
            progressBar.enabled = true;
        }

        progressBar.value += 0.01f;

        // Gradually reduce the volume of the current audio
        if (currentAudio != null && currentAudio.volume > 0f)
        {
            currentAudio.volume -= 0.01f; // Reduce volume by 0.01 per frame
            currentAudio.volume = Mathf.Clamp(currentAudio.volume, 0f, 1f); // Ensure volume doesn't go below 0
        }

        if (progressBar.value >= 1f)
        {
            Debug.Log("You win!");
            Won = true;
            progressBar.value = 0f;
            targetSinewave.Reroll();
            inputDeviceSwitcher.MouseOn(); // Set game over state to false
            scannerUI.SetActive(false);

            // Optionally stop the audio when the progress bar is full
            if (currentAudio != null)
            {
                currentAudio.Stop();
            }
        }
    }

    private void ProgressBarUnfilling()
    {
        // Gradually increases the volume of the current audio
        if (currentAudio != null && currentAudio.volume > 0f)
        {
            currentAudio.volume += 0.01f; // Reduce volume by 0.01 per frame
            currentAudio.volume = Mathf.Clamp(currentAudio.volume, 0f, 1f); // Ensure volume doesn't go below 0
        }
        progressBar.value -= 0.01f;

        if (progressBar.value <= 0f)
        {
            progressBar.value = 0f;
            progressBar.enabled = false;

            // Optionally reset the audio volume when the progress bar is empty
            if (currentAudio != null)
            {
                currentAudio.volume = 1f; // Reset volume to full
            }
        }
    }
}