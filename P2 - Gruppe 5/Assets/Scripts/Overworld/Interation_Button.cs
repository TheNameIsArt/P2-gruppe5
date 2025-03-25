using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Interation_Button : MonoBehaviour
{
    public Sprite keyboardSprite;
    public Sprite controllerSprite;
    public SpriteRenderer spriteRenderer;
    public Animator animator;
    public bool active;
    public GameObject interactionController;

    private string targetSceneName;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateControlScheme();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateControlScheme();
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
            spriteRenderer.sprite = keyboardSprite;
        }
        else if (currentControlScheme == "Gamepad")
        {
            spriteRenderer.sprite = controllerSprite;
        }
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
