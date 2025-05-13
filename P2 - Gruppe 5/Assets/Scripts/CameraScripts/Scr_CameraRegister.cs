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
}
