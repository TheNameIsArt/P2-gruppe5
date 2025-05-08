using DialogueEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player_Overworld_Controller : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rb;
    public Animator animator;
    public InputDevice inputDevice;
    private GameObject interactionZone;
    private PlayerInput playerInput;

    private string targetSceneName;
    //private NPCConversation targetConversation;
    private Vector2 moveInput;
    private GameObject interactionButton;
    private bool isConversationZone;
    private bool isInteractionZone;
    [SerializeField] private bool isConversationActive = false;

    private ConversationEditer conversationEditor; // Reference to the ConversationEditor

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        interactionButton = GameObject.FindGameObjectWithTag("InteractionButton");
        
    }

    void Start()
    {
        InputSystem.onActionChange += OnActionChange;
        if (interactionButton != null)
        {
            interactionButton.SetActive(false);
        }
        playerInput = GetComponent<PlayerInput>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.linearVelocity = moveInput * moveSpeed;
        Animate();


        if (ConversationManager.Instance != null)
        {
            if (!ConversationManager.Instance.IsConversationActive && !playerInput.enabled)
            {
                playerInput.enabled = true; // Enable the PlayerInput component
            }
        }
        if (isConversationActive) 
        {
            
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

    private void OnTriggerStay2D(Collider2D collision)
    {
        interactionZone = collision.gameObject;

        if (collision.gameObject.tag == "InteractionZone")
        {
            targetSceneName = interactionZone.GetComponent<Interaction_Controller>().targetSceneName;
            isInteractionZone = true;
            interactionButton.SetActive(true);
        }
        else if (collision.gameObject.tag == "ConversationZone")
        {
            //targetConversation = interactionZone.GetComponent<NPCConversation>();
            conversationEditor = interactionZone.GetComponent<ConversationEditer>();
            isConversationZone = true;
            if(!isConversationActive)
                interactionButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactionButton != null)
        {
            if (collision.gameObject.tag == "InteractionZone" || collision.gameObject.tag == "ConversationZone")
            {
                interactionButton.SetActive(false);
                isInteractionZone = false;
                isConversationZone = false;
            }
        }
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (interactionButton != null)
        {
            if (context.performed && isInteractionZone)
            {
                SceneManager.LoadScene(targetSceneName);
                Debug.Log("INTERACT!");
            }
            else if (context.performed && isConversationZone)
            {
                if (conversationEditor != null)
                {
                    conversationEditor.PlayConversation();
                    Debug.Log("Conversation started!");
                }
                playerInput.enabled = false; // Disable the PlayerInput component
                interactionButton.SetActive(false); // Hide the interaction button
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
