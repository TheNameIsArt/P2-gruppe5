using UnityEngine;
using UnityEngine.InputSystem;

public class CrosshairController : MonoBehaviour
{
    public RectTransform crosshair;
    public Camera mainCam;


    void Update()
    {
        Vector2 pos = Mouse.current.position.ReadValue();
        crosshair.position = pos;

    }

}
