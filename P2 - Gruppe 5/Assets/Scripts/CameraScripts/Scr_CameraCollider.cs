using UnityEngine;
using Cinemachine;

public class Scr_CameraCollider : MonoBehaviour
{
    public CinemachineVirtualCamera localCamera;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            Scr_CameraController.SwitchCamera(localCamera);
        }
    }
}
