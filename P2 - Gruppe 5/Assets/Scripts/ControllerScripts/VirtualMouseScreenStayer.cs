using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.UI;

public class VirtualMouseScreenStayer : MonoBehaviour
{
    private VirtualMouseInput virtualMouseInput;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        virtualMouseInput = GetComponent<VirtualMouseInput>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (virtualMouseInput != null && virtualMouseInput.enabled)
        {
            Vector2 mousePosition = virtualMouseInput.virtualMouse.position.value;
            mousePosition.x = Mathf.Clamp(mousePosition.x, 0, Screen.width);
            mousePosition.y = Mathf.Clamp(mousePosition.y, 0, Screen.height);
            InputState.Change(virtualMouseInput.virtualMouse.position, mousePosition);
        }
    }
}
