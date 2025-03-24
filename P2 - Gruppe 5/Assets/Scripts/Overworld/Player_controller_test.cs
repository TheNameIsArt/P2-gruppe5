using UnityEngine;
using UnityEngine.SceneManagement;

public class Player_controller : MonoBehaviour
{
    public float xInput;
    public float yInput;
    public float movespeed;
    public bool isFacingRight;
    public Rigidbody2D rb;
    public Animator animator;
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        isFacingRight = true;
        Scene currentScene = SceneManager.GetActiveScene();
        Debug.Log("Current scene:" + currentScene.name);
    }

    // Update is called once per frame
    void Update()
    {
        CheckInput();
        Flip();
        if (xInput != 0 || yInput != 0)
        {
            animator.Play("New_Guy_Run");
        }
        else 
        {
            animator.Play("New_Guy_Idle");
        }
    }
    private void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(xInput * movespeed, yInput * movespeed); 
    }

    void CheckInput() 
    {
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
    }
    void Flip() 
    {
        if (isFacingRight && xInput < 0f || !isFacingRight && xInput > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
