using UnityEngine;
using UnityEngine.InputSystem;

public class Button_test : MonoBehaviour
{
    public Transform player;
    private float spacing = 1.3f;
    private Animator animator;
    private Animation correctAnimation;
    private InputDevice inputDevice;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Interaction_Button_Keyboard");
        InputSystem.onActionChange += OnActionChange;
    }

    // Update is called once per frame
    void Update()
    {
        MoveWithPlayer();
        SelectAnimaton();
    }
    void MoveWithPlayer()
    {
        transform.position = new Vector3(player.position.x, player.position.y + spacing, player.position.z);
    }
    void SelectAnimaton() 
    {
        if (inputDevice is Keyboard)
        {
            animator.Play("Interaction_Button_Keyboard");
        }
        else if (inputDevice is Gamepad)
        {
            animator.Play("Interaction_Button_Controller");
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
