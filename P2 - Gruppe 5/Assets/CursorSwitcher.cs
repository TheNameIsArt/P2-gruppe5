using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class CursorSwitcher : MonoBehaviour
{
    public static CursorSwitcher instance;
    [SerializeField] private VirtualMouseInput virtualMouse;
    [SerializeField] private Image cursorImage;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        cursorImage = GetComponent<Image>();
        virtualMouse = GetComponent<VirtualMouseInput>();
        CheckMouseInput();

    }

    // Update is called once per frame
    void Update()
    {
        CheckMouseInput();
    }

    void CheckMouseInput()
    {
        if (Gamepad.current != null)
        {
            UnityEngine.Cursor.visible = false;
            if (virtualMouse != null && virtualMouse.enabled == false)
            {
                virtualMouse.enabled = true;
                Cursor.lockState = CursorLockMode.Locked;
            }
            cursorImage.enabled = true;
        }
        else
        {
            if (virtualMouse != null && virtualMouse.enabled == true)
            {
                virtualMouse.enabled = false;
            }
            UnityEngine.Cursor.visible = true;
            cursorImage.enabled = false;
        }
    }
}
