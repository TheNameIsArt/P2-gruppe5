using Cinemachine;
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
    public string playerName;
    public GameObject hats;
    private GameObject hideHats;
    public PlayerInput playerInput;

    private GameObject interactionZone;
    private string targetSceneName;
    //private NPCConversation targetConversation;
    public Vector2 moveInput;
    public GameObject interactionButton;
    private bool isConversationZone;
    private bool isInteractionZone;
    private bool isContextZone;
    private bool isDelilahConversationZone;

    [SerializeField] private bool isConversationActive = false;
    public CinemachineVirtualCamera localCamera;
    public CinemachineVirtualCamera localCamera2;

    private ConversationEditer conversationEditor;
    private DelilahConversation delilahConversation;// Reference to the ConversationEditor

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        if (interactionButton == null)
        {
            interactionButton = GameObject.FindGameObjectWithTag("InteractionButton");
        }

        playerInput = GetComponent<PlayerInput>();
        hideHats = GameObject.FindGameObjectWithTag("HideHat");
    }

    void Start()
    {
        //InputSystem.onActionChange += OnActionChange;
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
        if (ConversationManager.Instance != null)
        {
            if (!ConversationManager.Instance.IsConversationActive && !playerInput.enabled)
            {
                playerInput.enabled = true; // Enable the PlayerInput component
            }
        }
        if (ConversationManager.Instance != null)
        {
            if (ConversationManager.Instance.IsConversationActive)
            {
                Scr_CameraController.SwitchCamera(localCamera);
                //Debug.Log("Camera switched to localCamera");
                if (playerInput != null)
                {
                    playerInput.enabled = false; // Disable the PlayerInput component
                }
            }
            else if (!ConversationManager.Instance.IsConversationActive)
            {
                Scr_CameraController.SwitchCamera(localCamera2);
                //Debug.Log("Camera switched to localCamera2");
                if (playerInput != null)
                {
                    playerInput.enabled = true; // Enable the PlayerInput component
                }
            }
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

        if (playerName == ("Andy")) 
        {
            if (moveInput.x != 0 || moveInput.y != 0)
            {
                animator.Play("Andy_Run");
                
                
            }
            else
            {
                animator.Play("Andy_Idle");
                
                
            }
        }
        else if (playerName == "Delilah")
        {
            if (moveInput.x != 0 || moveInput.y != 0)
            {
                animator.Play("Delilah_Run");
                hats.GetComponent<Scr_HatSwitcher>().HatRun();
            }
            else
            {
                animator.Play("Delilah_Idle");
                hats.GetComponent<Scr_HatSwitcher>().HatIdle();
            }
        }
        else if (playerName == "Sam")
        {
            if (moveInput.x != 0 || moveInput.y != 0)
            {
                animator.Play("Sam_Run");
            }
            else
            {
                animator.Play("Sam_Idle");
            }
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
        else if (collision.gameObject.tag == "ContextZone")
        {
            isContextZone = true;
            interactionButton.SetActive(true);
        }
        else if (collision.gameObject.tag == "DelilahConversationZone")
        {
            //targetConversation = interactionZone.GetComponent<NPCConversation>();
            delilahConversation = interactionZone.GetComponent<DelilahConversation>();
            isDelilahConversationZone = true;
            if (!isConversationActive)
                interactionButton.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (interactionButton != null)
        {
            if (collision.gameObject.tag == "InteractionZone" || collision.gameObject.tag == "ConversationZone" || collision.gameObject.tag == "ContextZone" || collision.gameObject.tag == "DelilahConversationZone")
            {
                interactionButton.SetActive(false);
                isInteractionZone = false;
                isConversationZone = false;
                isContextZone = false;
                isDelilahConversationZone = false;
            }
        }
       
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (interactionButton != null)
        {
            /*if (context.performed && isInteractionZone)
            {
                SceneManager.LoadScene(targetSceneName);
                Debug.Log("INTERACT!");
            }*/
            if (context.performed && isConversationZone)
            {
                if (conversationEditor != null)
                {
                    conversationEditor.PlayConversation();
                    Debug.Log("Conversation started!");
                }
                playerInput.enabled = false; // Disable the PlayerInput component
                interactionButton.SetActive(false); // Hide the interaction button
            }
            else if (context.performed && isContextZone)
            {
                Debug.Log("Context interaction!");

                // Check if the interactionZone has a script attached
                if (interactionZone != null)
                {
                    // Try to find a script attached to the interactionZone
                    var contextScript = interactionZone.GetComponent<MonoBehaviour>();

                    if (contextScript != null)
                    {
                        // Check if the script has a method called "PerformAction"
                        var method = contextScript.GetType().GetMethod("PerformAction");
                        if (method != null)
                        {
                            // Invoke the "PerformAction" method on the script
                            method.Invoke(contextScript, null);
                            Debug.Log($"Performed action on {contextScript.GetType().Name}");
                        }
                        else
                        {
                            Debug.LogWarning($"No 'PerformAction' method found on {contextScript.GetType().Name}");
                        }
                    }
                    else
                    {
                        Debug.LogWarning("No script found on the ContextZone GameObject.");
                    }
                }
            }
            else if (context.performed && isDelilahConversationZone)
            {
                if (delilahConversation != null)
                {
                    delilahConversation.PlayConversation();
                    Debug.Log("Delilah Conversation started!");
                }
                playerInput.enabled = false; // Disable the PlayerInput component
                interactionButton.SetActive(false); // Hide the interaction button
            }
        }
    }

    /*public void Sprint(InputAction.CallbackContext context)
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
    }*/
}
