using UnityEngine;
using UnityEngine.InputSystem;

public class FoodObject : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    public Sprite defaultImage;
    public Sprite pressedImage;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        
    }

    public void ButtonPress(InputAction.CallbackContext context)
    {
        //Debug.Log("Input performed!");
        if (context.performed)
        {
            spriteRenderer.sprite = pressedImage;
        }
    }

    public void ButtonRelease(InputAction.CallbackContext context)
    {
        //Debug.Log("Button released");
        if (context.performed)
        {
            spriteRenderer.sprite = defaultImage;
        }
    }
}
