using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class FlickerLight : MonoBehaviour
{
    public Light2D lightSource;

    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.05f;
    public float flickertime = 0.5f; // Duration of the flicker effect

    void Start()
    {
        StartCoroutine(FlickerRoutine());
    }

    IEnumerator FlickerRoutine()
    {
        while (true)
        {
            // Flicker for flickertime seconds
            float elapsed = 0f;
            while (elapsed < flickertime)
            {
                lightSource.intensity = Random.Range(minIntensity, maxIntensity);
                yield return new WaitForSeconds(flickerSpeed);
                elapsed += flickerSpeed;
            }
            // Optionally, set intensity to default after flicker
            lightSource.intensity = maxIntensity;

            // Wait for a random interval between 5 and 10 seconds before next flicker
            float waitTime = Random.Range(5f, 10f);
            yield return new WaitForSeconds(waitTime);
        }
    }
}
