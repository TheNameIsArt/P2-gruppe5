using UnityEngine;

public class Bot_Animation : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private AnimationClip idleAnimation;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        animator = GetComponent<Animator>();
        animator.Play(idleAnimation.name);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
