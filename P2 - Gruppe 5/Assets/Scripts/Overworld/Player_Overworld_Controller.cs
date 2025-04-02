using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_Overworld_Controller : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public InputDevice inputDevice;
    public GameObject interactionZone;

    private string targetSceneName;
    private Vector2 moveInput;
    private GameObject interactionButton;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactionButton = GameObject.FindGameObjectWithTag("InteractionButton");
        InputSystem.onActionChange += OnActionChange;
        if (interactionButton != null)
        {
            interactionButton.SetActive(false);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        Animate();
        if (interactionButton != null) 
        {
            targetSceneName = interactionZone.GetComponent<Interaction_Controller>().targetSceneName;
            //targetSceneName = GameObject.FindGameObjectWithTag("InteractionZone").GetComponent<Interaction_Controller>().targetSceneName;
        }

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
        if (interactionButton != null) 
        {
            if (collision.gameObject.tag == "InteractionZone")
            {
                interactionButton.SetActive(false);
            }
        }
        
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (interactionButton != null)
        {
            if (context.performed)
            {
                // if possible change this code to use a metod from the interaction zone, instead of directly loading the scene.
                // this way we can do other things than just loading a scene (eg. in-world dialouge ect.).
                SceneManager.LoadScene(targetSceneName);
                Debug.Log("INTERACT!");
            }
        }
    }
    public void Sprint(InputAction.CallbackContext context) 
    {
        if (context.performed)
        {
            Debug.Log("Sprint!");
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
