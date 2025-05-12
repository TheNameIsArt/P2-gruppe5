using UnityEngine;
using Cinemachine;

public class Crowd_CameraCollider : MonoBehaviour
{
    public CinemachineVirtualCamera localCamera;
    public PlayerMovement_crowd playerMovement; // Reference to PlayerMovement_crowd

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Tail"))
        {
            Crowd_CameraController.SwitchCamera(localCamera);
        }
    }
}
