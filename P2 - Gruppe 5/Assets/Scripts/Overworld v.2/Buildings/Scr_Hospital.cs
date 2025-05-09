using UnityEngine;

public class Scr_Hospital : MonoBehaviour
{
    private Animator animator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play("Hospital");

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
