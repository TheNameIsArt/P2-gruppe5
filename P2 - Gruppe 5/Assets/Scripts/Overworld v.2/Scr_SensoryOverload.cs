using UnityEngine;
using Cinemachine;

public class Scr_SensoryOverload : MonoBehaviour
{
    public CinemachineVirtualCamera localCamera;
    private float shakeIntensity = 5f;
    private float shakeDuration = 2f;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            ShakeScreen(shakeIntensity);
            DistortSound();
            Debug.Log("Sensory Overload Triggered");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        localCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
    }
    private void ShakeScreen(float intensity) 
    {
        localCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
        Debug.Log("Shake Screen");
    }
    private void DistortSound()
    {
        Debug.Log("Distort Sound");
    }
}
