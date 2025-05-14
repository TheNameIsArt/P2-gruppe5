using UnityEngine;

public class Scr_HatSwitcher : MonoBehaviour
{
    public Animator animator; 
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.Play("Hat_Idle_Construction");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
