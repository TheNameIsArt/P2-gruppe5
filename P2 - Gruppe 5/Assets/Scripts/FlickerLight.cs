using UnityEngine;
using System.Collections;
using UnityEngine.Rendering.Universal;

public class FlickerLight : MonoBehaviour
{
    public Light2D lightSource; // Assign your Light2D or Light component here

    public float minIntensity = 0.5f;
    public float maxIntensity = 1.5f;
    public float flickerSpeed = 0.05f;
    public float flickertime = 0.5f; // Duration of the flicker effect

    void Start()
    {
        StartCoroutine(Flicker());
    }

    IEnumerator Flicker()
    {
        float elapsed = 0f;
        while (elapsed < flickertime)
        {
            lightSource.intensity = Random.Range(minIntensity, maxIntensity);
            yield return new WaitForSeconds(flickerSpeed);
            elapsed += flickerSpeed;
        }
        // Optionally, set the intensity to a default value after flickering
        lightSource.intensity = maxIntensity;
    }
}
