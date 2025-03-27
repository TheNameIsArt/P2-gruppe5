using UnityEngine;
using UnityEngine.InputSystem;

public class BurgerBar : MonoBehaviour
{
    private GameObject interactionButton;
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        interactionButton = GameObject.FindGameObjectWithTag("InteractionButton");
        animator.Play("BurgerBar_Idle");

    }

    // Update is called once per frame
    void Update()
    {
        interactionButton = GameObject.FindGameObjectWithTag("InteractionButton");
        Animate();
    }
    void Animate() 
    {
        //animator.Play("BurgerBar_Idle");
        if (interactionButton != null) 
        {
            if (interactionButton.activeSelf)
            {
                animator.Play("BurgerBar_Interact");
            }

        }
        else
        {
            animator.Play("BurgerBar_Idle");
        }

    }
}
