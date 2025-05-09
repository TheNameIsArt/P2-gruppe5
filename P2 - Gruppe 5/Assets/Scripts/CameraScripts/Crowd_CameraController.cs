using UnityEngine;
using Cinemachine;
using NUnit.Framework;
using System.Collections.Generic;

public class Crowd_CameraController : MonoBehaviour
{
 
    static List<CinemachineVirtualCamera> cameras = new List<CinemachineVirtualCamera>();

    public static CinemachineVirtualCamera ActiveCamera = null;


    public static bool IsCameraActive(CinemachineVirtualCamera camera)
    {
        return camera == ActiveCamera;
    }

    public static void SwitchCamera(CinemachineVirtualCamera newCamera)
    {
        newCamera.Priority = 10;
        ActiveCamera = newCamera;

        foreach (CinemachineVirtualCamera camera in cameras)
        {
            if (camera != newCamera)
            {
                camera.Priority = 0;
            }
        }
    }

    public static void RegisterCamera(CinemachineVirtualCamera camera)
    {
            cameras.Add(camera);
    }

    public static void UnregisterCamera(CinemachineVirtualCamera camera)
    {
            cameras.Remove(camera);
    }
}
