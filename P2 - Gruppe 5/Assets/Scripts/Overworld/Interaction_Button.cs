using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Interaction_Button : MonoBehaviour
{
    //public Sprite keyboardSprite;
    //public Sprite controllerSprite;
    //public SpriteRenderer spriteRenderer;
    public Animator animator;
    public GameObject interactionController;
    public Transform player;
    public float spacing = 5f;

    string targetSceneName;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateControlScheme();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateControlScheme();
        MoveWithPlayer();
        if (gameObject.activeSelf)
        {
            targetSceneName = GameObject.FindGameObjectWithTag("InteractionZone").GetComponent<Interaction_Controller>().targetSceneName;
        }
    }
    void UpdateControlScheme()
    {
        var currentControlScheme = InputSystem.devices[0].description.deviceClass;

        if (currentControlScheme == "Keyboard" || currentControlScheme == "Mouse")
        {
            animator.Play("Interaction_Button_Keyboard");
            //spriteRenderer.sprite = keyboardSprite;
        }
        else if (currentControlScheme == "Gamepad")
        {
            animator.Play("Interaction_Button_Controller");
            //spriteRenderer.sprite = controllerSprite;
        }
    }

    void MoveWithPlayer() 
    {
        transform.position = new Vector3(player.position.x, player.position.y + spacing,player.position.z);
    }

    public void Interact(InputAction.CallbackContext context)
    {
        if (gameObject.activeSelf)
        {
            if (context.performed)
            {
                SceneManager.LoadScene(targetSceneName);
            }
        }
       
    }
}
