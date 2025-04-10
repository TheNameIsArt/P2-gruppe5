using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite defaultImage; //Default billede af knappen man skal trykke på
    public Sprite PressedImage; //Billede af knappen efter der er trykket. Virker om visual feedback.

    public KeyCode KeyToPress;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //Finder SpriteRenderen
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void ButtonPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            spriteRenderer.sprite = PressedImage; //Hvis der trykkes skifter billedet
        }
    }

    public void ButtonRelease(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            spriteRenderer.sprite = defaultImage; //Giver man slip skifter billedet tilbage
        }
    }
}
