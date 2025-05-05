using UnityEngine;

public class Scr_Building : MonoBehaviour
{
    public Animator animator;
    public AnimationClip Building;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator.Play(Building.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
