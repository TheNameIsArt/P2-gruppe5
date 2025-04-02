using UnityEngine;
using UnityEngine.InputSystem;

public class BurgerBar : MonoBehaviour
{
    
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("BurgerBar_Idle");

    }

    // Update is called once per frame
    void Update()
    {

    }
}
