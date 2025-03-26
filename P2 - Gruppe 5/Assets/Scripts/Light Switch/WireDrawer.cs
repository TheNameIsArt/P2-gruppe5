using UnityEngine;
using UnityEngine.EventSystems;

public class WireDrawer : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private LineRenderer lineRenderer;
    private bool isDragging = false;
    private Vector3 startPosition;
    private WireManager wireManager; // Reference to WireManager
    public GameObject connectedEnd; // Store the connected endpoint

    public RectTransform canvasRect; // Reference to UI Canvas
    public float snapRadius = 50f; // Maximum snapping distance

    private void Start()
    {
        wireManager = GameObject.Find("Wire Manager").GetComponent<WireManager>(); // Automatically find the manager

        // Create and configure LineRenderer
        lineRenderer = gameObject.AddComponent<LineRenderer>();
        lineRenderer.startWidth = 0.05f;
        lineRenderer.endWidth = 0.05f;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        // Set Material (Optional)
        lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
        lineRenderer.startColor = Color.white;
        lineRenderer.endColor = Color.white;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        isDragging = true;

        // Get the button's RectTransform position in world space
        RectTransform buttonRect = GetComponent<RectTransform>();
        Vector3 buttonWorldPos = buttonRect.position;

        // Set the line's start position correctly
        lineRenderer.SetPosition(0, buttonWorldPos);
    }

    private void Update()
    {
        if (isDragging)
        {
            // Convert mouse position to world space
            Vector3 mouseWorldPos;
            RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, Input.mousePosition, Camera.main, out mouseWorldPos);

            lineRenderer.SetPosition(1, mouseWorldPos);
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isDragging = false;

        Vector3 endPosition;
        RectTransformUtility.ScreenPointToWorldPointInRectangle(canvasRect, Input.mousePosition, eventData.pressEventCamera, out endPosition);

        GameObject closestEnd = FindClosestEndPoint(endPosition);

        if (closestEnd != null)
        {
            lineRenderer.SetPosition(1, closestEnd.transform.position);
            connectedEnd = closestEnd;

            Debug.Log($" Wire connected to: {connectedEnd.name}");

            // **Notify WireManager**
            if (wireManager != null)
            {
                wireManager.RegisterWire(gameObject, connectedEnd);
            }
            else
            {
                Debug.LogError(" WireManager is NULL in WireDrawer!");
            }
        }
        else
        {
            Debug.Log(" No valid endpoint found. Destroying wire.");
            Destroy(lineRenderer);
        }
    }

    private GameObject FindClosestEndPoint(Vector3 position)
    {
        GameObject[] endPoints = GameObject.FindGameObjectsWithTag("WireEnd");
        GameObject closest = null;
        float minDistance = snapRadius;

        foreach (GameObject end in endPoints)
        {
            float distance = Vector3.Distance(position, end.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = end;
            }
        }
        return closest; // Returns null if no endpoint is within range
    }

    void ConnectWire(GameObject endPoint)
    {
        connectedEnd = endPoint; // Store the connected endpoint

        if (wireManager != null)
        {
            wireManager.RegisterWire(gameObject, connectedEnd); // Pass both start and end points
            Debug.Log($" Wire connected: {gameObject.name} -> {connectedEnd.name}");
        }
        else
        {
            Debug.LogError(" WireManager is NULL! Make sure WireDrawer has a reference.");
        }
    }
}