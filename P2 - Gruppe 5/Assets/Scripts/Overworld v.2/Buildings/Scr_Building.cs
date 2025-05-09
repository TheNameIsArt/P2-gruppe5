using UnityEngine;

public class Scr_Building : MonoBehaviour
{
    private Animator animator;
    public AnimationClip Building;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play(Building.name);
    }
}