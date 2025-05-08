using UnityEngine;

public class Scr_HatSwitch : MonoBehaviour
{
    [SerializeField] private SpriteRenderer hatRenderer;
    public Sprite[] Hats;

    public string hatKey;
    public int hatChooser;
    //public bool hatEnabled = false;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        hatRenderer = gameObject.GetComponent<SpriteRenderer>();
        hatRenderer.enabled = false;
        //Debug.Log(Hats.Length);

    }

    // Update is called once per frame
    void Update()
    {
        //HatEnabler();
        HatChooser();
        HatDetector();

    }

    void HatDetector()
    {
        if (Input.GetKeyDown(KeyCode.T) /*&& hatRenderer.enabled*/)
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

    void HatEnabler()
    {
        if (Input.GetKeyDown(KeyCode.E) && !hatRenderer.enabled)
        {
            hatRenderer.enabled = true;
            hatChooser = 0;
        }
    }

    void HatChooser()
    {
        switch (hatChooser)
        {
            case 0:
                HatSelector(hatChooser);
                hatKey = "Hat: " + hatChooser;
                //hatRenderer.enabled = false;
                Debug.Log("No hat equipped");
                break;
            case 1:
                HatSelector(hatChooser);
                hatKey = "Hat: " + hatChooser;
                Debug.Log(hatKey);
                break;
            case 2:
                HatSelector(hatChooser);
                hatKey = "Hat: " + hatChooser;
                Debug.Log(hatKey);
                break;
            case 3:
                HatSelector(hatChooser);
                hatKey = "Hat: " + hatChooser;
                Debug.Log(hatKey);
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
