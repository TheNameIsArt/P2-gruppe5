using UnityEngine;
using UnityEngine.UI;

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
    }

    private void Update()
    {

        //Mouse controls
        float mouseX = Input.GetAxis("Mouse X");

        if (mouseX < 0)
        {
            //Debug.Log("Mouse moved left!");
            currentFrequenzy = controledSinewave.frequency + frequenzyChanger;

            if (currentFrequenzy > 1.5f)
            {
                currentFrequenzy = 1.5f;
            }
            controledSinewave.frequency = currentFrequenzy;
        }
        else if (mouseX > 0)
        {
            //Debug.Log("Mouse moved right!");
            currentFrequenzy = controledSinewave.frequency - frequenzyChanger;

            if (currentFrequenzy < 0f)
            {
                currentFrequenzy = 0f;
            }
            controledSinewave.frequency = currentFrequenzy;
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("W key is held down!");
            currentAmplitude = controledSinewave.amplitude + amplitudeChanger;
            if (currentAmplitude > 1f)
            {
                currentAmplitude = 1f;
            }
            controledSinewave.amplitude = currentAmplitude;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //Debug.Log("S key is held down!");
            currentAmplitude = controledSinewave.amplitude - amplitudeChanger;
            if (currentAmplitude < 0f)
            {
                currentAmplitude = 0f;
            }
            controledSinewave.amplitude = currentAmplitude;
        }

        if (Mathf.Abs(targetSinewave.amplitude - currentAmplitude) <= 0.05f && Mathf.Abs(targetSinewave.frequency - currentFrequenzy) <= 0.01f)
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

        if (progressBar.value >= 1f)
        {
            
            Debug.Log("You win!");
            Won = true;
            progressBar.value = 0f;
            targetSinewave.Reroll();
            scannerUI.SetActive(false);
        }
    }

    private void ProgressBarUnfilling()
    {
        progressBar.value -= 0.01f; // Decrease the value

        if (progressBar.value <= 0f) // Disable only when value reaches 0
        {
            progressBar.value = 0f; // Ensure it doesn’t go below 0
            progressBar.enabled = false;
        }
    }
}
