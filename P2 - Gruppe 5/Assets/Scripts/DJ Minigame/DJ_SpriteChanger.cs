using UnityEditor.ProjectWindowCallback;
using UnityEngine;
using UnityEngine.InputSystem;

public class DJ_SpriteChanger : MonoBehaviour
{
    private Animator animator;
    public AnimationClip keyboard;
    public AnimationClip controller;
    private InputDevice inputDevice;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(keyboard.name);
        InputSystem.onActionChange += OnActionChange;
    }

    // Update is called once per frame
    void Update()
    {
        SelectAnimaton();
    }
    void SelectAnimaton()
    {
        if (inputDevice is Keyboard)
        {
            animator.Play(keyboard.name);
        }
        else if (inputDevice is Gamepad)
        {
            animator.Play(controller.name);
        }
    }
    private void OnActionChange(object obj, InputActionChange change)
    {
        if (change == InputActionChange.ActionPerformed)
        {
            var inputAction = (InputAction)obj;
            var lastControl = inputAction.activeControl;
            inputDevice = lastControl.device;
        }

    }

}
