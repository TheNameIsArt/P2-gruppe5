using UnityEngine;
using Cinemachine;

public class Scr_CameraRegister : MonoBehaviour
{
    private void OnEnable()
    {
        Crowd_CameraController.RegisterCamera(GetComponent<CinemachineVirtualCamera>());
    }

    private void OnDisable()
    {
        Crowd_CameraController.UnregisterCamera(GetComponent<CinemachineVirtualCamera>());
    }
}
