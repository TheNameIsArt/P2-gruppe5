using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class LookerManager : MonoBehaviour
{

    //Find all gameobjects inhereting from Lookers
    private GameObject Lookers;
    private Vector2 pushForce;
    private Vector2 objectPosition;
    private Vector2 deltaVector;
    private Vector2 mousePosition = Mouse.current.position.ReadValue();
    private Vector2 newMousePosition;
    private Tasklist tasklist;
    private Collider collider;

    //Track which ones are selected and have the cursor close to
    //Randomize timer you need to hold eye contact
    //Apply push force on cursor away from selected task that player needs to resist



    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        collider = Lookers.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnMouseOver()
    {
        
        /*objectPosition = transform.position;
        deltaVector = mousePosition - objectPosition;
        deltaVector.Normalize();
        newMousePosition = mousePosition + deltaVector * -deltaVector;

        Mouse.current.WarpCursorPosition(newMousePosition);*/

    }

}
