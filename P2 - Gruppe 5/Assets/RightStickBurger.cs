using UnityEngine;
using UnityEngine.InputSystem;

public class SingleAnimationUniversal : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private Animator myAnimator;
    [SerializeField] private AnimationClip gamepadAnimation;
    [SerializeField] private AnimationClip keyboardAnimation;
    InputDeviceSwitcher inputDeviceSwitcher;
    private InputDevice inputDevice;
    void Start()
    {
        myAnimator = GetComponent<Animator>();

        inputDeviceSwitcher = GameObject.Find("Cursor Canvas").GetComponent<InputDeviceSwitcher>();
        inputDevice = inputDeviceSwitcher.inputDevice;

       
    }
    private void Update()
    {
        inputDevice = inputDeviceSwitcher.inputDevice;
        if (inputDeviceSwitcher == null)
        {
            myAnimator.Play(keyboardAnimation.name);
            return;
        }

        if (inputDeviceSwitcher.inputDevice != null && inputDeviceSwitcher.inputDevice.name == "VirtualMouse")
        {
            Debug.Log("Virtual mouse detected. Skipping animation.");
            return;
        }

        Debug.Log("InputDeviceSwitcher found on Cursor object.");
        if (inputDevice is Gamepad)
        {
            myAnimator.Play(gamepadAnimation.name);
        }
        else if (inputDeviceSwitcher.inputDevice is Keyboard or Mouse)
        {
            myAnimator.Play(keyboardAnimation.name);
        }
        else
        {
            Debug.Log("Other input device detected: " + inputDeviceSwitcher.inputDevice.displayName);
        }
    }
}
