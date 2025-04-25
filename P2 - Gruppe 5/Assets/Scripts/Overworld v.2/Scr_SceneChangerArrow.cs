using UnityEngine;

public class Scr_SceneChangerArrow : MonoBehaviour
{
    public string direction;
    public SpriteRenderer spriteRenderer;

    private Animator animator;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
       animator = GetComponent<Animator>();
       spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
            ChangeAnimation();
    }

    private void ChangeAnimation() 
    {
        if (direction == "UP")
        {
            animator.Play("SceneChangeArrowUP");
        }
        else if (direction == "DOWN")
        {
            animator.Play("SceneChangeArrowDOWN");
        }
        else if (direction == "LEFT")
        {
            animator.Play("SceneChangeArrowLEFT");
        }
        else if (direction == "RIGHT")
        {
            animator.Play("SceneChangeArrowRIGHT");
        }
        else 
        {
            animator.Play("Empty");
        }
    }
}
