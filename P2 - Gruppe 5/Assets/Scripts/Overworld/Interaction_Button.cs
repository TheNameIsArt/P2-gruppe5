using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Interaction_Button : MonoBehaviour
{
    public Animator animator;
    public GameObject interactionController;
    public Transform player;
    public float spacing = 5f;

    private string targetSceneName;
    private string lastDevice;
    private GameObject _player;

    void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        gameObject.SetActive(false);
    }

    void Update()
    {
        MoveWithPlayer();

        
        if (gameObject.activeSelf)
        {
            targetSceneName = GameObject.FindGameObjectWithTag("InteractionZone").GetComponent<Interaction_Controller>().targetSceneName;
        }
        ChangeUI();
    }

    void MoveWithPlayer()
    {
        transform.position = new Vector3(player.position.x, player.position.y + spacing, player.position.z);
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
    
    void ChangeUI() 
    {
        if (lastDevice == "Gamepad")
        {
            animator.Play("Interaction_Button_Controller");
        }
        else if (lastDevice == "Keyboard")
        {
            animator.Play("Interaction_Button_Keyboard");
        }
    }
}
