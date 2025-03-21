using UnityEngine;

public class SineManipulator : MonoBehaviour

{
    public Sinewave Sinewave;
    [SerializeField]
    private float currentAmplitude;
    private float currentFrequenzy;
    private float amplitudeChanger = 0.01f;
    private float frequenzyChanger = 0.01f;

    private void Start()
    {
        float randomValue1 = Random.Range(0.0f, 1.0f);
        float randomValue2 = Random.Range(0.0f, 1.0f);
        Debug.Log("Random Floats: " + randomValue1 + " and " + randomValue2);

        Sinewave.frequency = randomValue1;
        Sinewave.amplitude = randomValue2;
    }

    private void Update()
    {

        //Mouse controls
        float mouseX = Input.GetAxis("Mouse X");

        if (mouseX < 0)
        {
            //Debug.Log("Mouse moved left!");
            currentFrequenzy = Sinewave.frequency + frequenzyChanger;

            if (currentFrequenzy > 1.5f)
            {
                currentFrequenzy = 1.5f;
            }
            Sinewave.frequency = currentFrequenzy;
        }
        else if (mouseX > 0)
        {
            //Debug.Log("Mouse moved right!");
            currentFrequenzy = Sinewave.frequency - frequenzyChanger;

            if (currentFrequenzy < 0f)
            {
                currentFrequenzy = 0f;
            }
            Sinewave.frequency = currentFrequenzy;
        }
    }
    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //Debug.Log("W key is held down!");
            currentAmplitude = Sinewave.amplitude + amplitudeChanger;
            if (currentAmplitude > 1f)
            {
                currentAmplitude = 1f;
            }
            Sinewave.amplitude = currentAmplitude;
        }

        if (Input.GetKey(KeyCode.S))
        {
            //Debug.Log("S key is held down!");
            currentAmplitude = Sinewave.amplitude - amplitudeChanger;
            if(currentAmplitude < 0f)
            {
                currentAmplitude = 0f;
            }
            Sinewave.amplitude = currentAmplitude;
        }
    }
}
