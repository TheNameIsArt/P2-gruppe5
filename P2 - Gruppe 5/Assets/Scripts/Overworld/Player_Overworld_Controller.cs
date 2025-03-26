using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_Overworld_Controller : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public InputDevice inputDevice;

    private string targetSceneName;
    private Vector2 moveInput;
    private GameObject interactionButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionButton = GameObject.FindGameObjectWithTag("InteractionButton");
        InputSystem.onActionChange += OnActionChange;
        interactionButton.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        Animate();
        targetSceneName = GameObject.FindGameObjectWithTag("InteractionZone").GetComponent<Interaction_Controller>().targetSceneName;
        //Debug.Log(inputDevice);
    }

    public void Move(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    void Animate()
    {
        if (moveInput.x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (moveInput.x != 0 || moveInput.y != 0)
        {
            animator.Play("New_Guy_Run");
        }
        else
        {
            animator.Play("New_Guy_Idle");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InteractionZone")
        {
            interactionButton.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "InteractionZone")
        {
            interactionButton.SetActive(false);
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (interactionButton.activeSelf)
        {
            if (context.performed)
            {
                SceneManager.LoadScene(targetSceneName);
                Debug.Log("INTERACT!");
            }
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
