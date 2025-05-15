using UnityEngine;

public class Scr_HatAnimator : MonoBehaviour
{
    public AnimationClip hat;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);

        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        animator.Play(hat.name);
        
    }
    public void HideHat() 
    {
        spriteRenderer.color = new Color(1f, 1f, 1f, 0f);
    }

}
