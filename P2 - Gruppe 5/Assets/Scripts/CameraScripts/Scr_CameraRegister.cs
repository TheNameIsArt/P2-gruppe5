using UnityEngine;
using Cinemachine;

public class Scr_CameraRegister : MonoBehaviour
{
    private void OnEnable()
    {
        Scr_CameraController.RegisterCamera(GetComponent<CinemachineVirtualCamera>());
    }

    private void OnDisable()
    {
        Scr_CameraController.UnregisterCamera(GetComponent<CinemachineVirtualCamera>());
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
