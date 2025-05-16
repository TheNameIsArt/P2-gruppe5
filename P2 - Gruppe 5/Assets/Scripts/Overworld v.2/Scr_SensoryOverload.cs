using UnityEngine;
using Cinemachine;

public class Scr_SensoryOverload : MonoBehaviour
{
    public CinemachineVirtualCamera localCamera;
    public CinemachineVirtualCamera localCamera2;
    public AudioSource localAudioSource;

    private float shakeIntensity = 5f;

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
            ShakeConvoScreen(shakeIntensity);
            DistortSound();
            Debug.Log("Sensory Overload Triggered");
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        localCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        localCamera2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0;
        localAudioSource.pitch = 1f;
    }
    private void ShakeScreen(float intensity) 
    {
        localCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
      
        Debug.Log("Shake Screen");
    }
    private void ShakeConvoScreen(float intensity) 
    {
        localCamera2.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = intensity;
    }
    private void DistortSound()
    {
        localAudioSource.pitch = -3f;
        Debug.Log("Distort Sound");
    }
}
