using UnityEngine;
using UnityEngine.InputSystem;

public class BurgerBar : MonoBehaviour
{
    
    private Animator animator;
    public bool interactable;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        interactable = false;
        animator = GetComponent<Animator>();
        animator.Play("BurgerBar_Idle");
    }

    // Update is called once per frame
    void Update()
    {
        if (interactable)
        {
            animator.Play("BurgerBar_Interact");
        }
    }
}
