using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class Scr_HatSwitch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hatRenderer;
    public Sprite[] Hats;

    public string hatKey;
    public int hatChooser;

    void Start()
    {
        hatRenderer = gameObject.GetComponent<SpriteRenderer>();
        hatRenderer.enabled = false;

    }

    void Update()
    {
        HatChooser();
    }

    public void HatIncreaser(InputAction.CallbackContext trigger)
    {
        if (trigger.performed)
        {
            hatRenderer.enabled = true;
            hatChooser = hatChooser + 1;
            Debug.Log(hatChooser);
            if (hatChooser >= Hats.Length)
            {
                hatChooser = 0;
            }
        }
    }

    void HatChooser()
    {
        switch (hatChooser)
        {
            case 0:
                HatSelector(hatChooser);
                hatKey = "Hat: " + hatChooser;
                break;
            case 1:
                HatSelector(hatChooser);
                hatKey = "Hat: " + hatChooser;
                break;
            case 2:
                HatSelector(hatChooser);
                hatKey = "Hat: " + hatChooser;
                break;
            case 3:
                HatSelector(hatChooser);
                hatKey = "Hat: " + hatChooser;
                break;
            case 4:
                HatSelector(hatChooser);
                hatKey = "Hat: " + hatChooser;
                break;
            default:
                hatRenderer.enabled = false;
                break;

        }
    }

    void HatSelector(int hatChooser)
    {
        hatRenderer.sprite = Hats[hatChooser];
    }

}
