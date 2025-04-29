using UnityEngine;
using UnityEngine.InputSystem;

public class ButtonController : MonoBehaviour
{

    private SpriteRenderer spriteRenderer;
    public Sprite defaultImage; //Default billede af knappen man skal trykke på
    public Sprite pressedImage; //Billede af knappen efter der er trykket. Virker om visual feedback.
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); //Finder SpriteRenderen
    }
    void Update()
    {
       
    }

    public void ButtonPress(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            spriteRenderer.sprite = pressedImage; //Hvis der trykkes skifter billedet
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
