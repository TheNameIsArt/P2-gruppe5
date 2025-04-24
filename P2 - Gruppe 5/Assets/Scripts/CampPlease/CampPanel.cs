using UnityEngine;

public class CampPanel : MonoBehaviour
{
    public Sprite[] panel; // [0] = default, [1] = hover
    private SpriteRenderer spriteRenderer;

    public bool hovering;
    private bool clicked;

    public bool panelActivated;

    [SerializeField] private GameObject[] mapAreas;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = panel[0];
        panelActivated = false;
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (hit.collider == null)
            {
                ResetPanel();
            }
            else
            {
                // Prevent reset if clicking on a CampButton
                if (hit.collider.GetComponent<CampAreaButton>() == null &&
                    hit.collider.gameObject != gameObject)
                {
                    ResetPanel();
                }
            }
        }
    }


    void OnMouseEnter()
    {
        hovering = true;
        if (!clicked)
            spriteRenderer.sprite = panel[1];
    }

    void OnMouseExit()
    {
        hovering = false;
        if (!clicked)
            spriteRenderer.sprite = panel[0];
    }

    void OnMouseDown()
    {
        clicked = true;
        spriteRenderer.sprite = panel[1];
        panelActivated = true;
        foreach (GameObject area in mapAreas)
        {
            area.SetActive(true);
        }
    }

    void ResetPanel()
    {
        clicked = false;
        panelActivated = false;
        spriteRenderer.sprite = panel[0];
        foreach (GameObject area in mapAreas)
        {
            area.SetActive(false);
        }
    }
}
