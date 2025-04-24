using UnityEngine;
using UnityEngine.UI;

public class CursorScript : MonoBehaviour
{
    //public float clickRange = 50f; // Max distance to click a button
    public float moveSpeed = 10f; // Speed of object movement

    void Start()
    {
        Cursor.visible = false; // Hide the cursor
        
    }

    void Update()
    {
        MoveWithMouse();
    }

    void MoveWithMouse()
    {
        Vector3 targetPosition = GetMouseWorldPosition();
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * moveSpeed);
    }

    Vector3 GetMouseWorldPosition()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane + 5f; // Adjust depth
        return Camera.main.ScreenToWorldPoint(mousePos);
    }

   /* Button FindClosestButtonInRange()
    {
        Button[] buttons = FindObjectsOfType<Button>();
        Button closest = null;
        float minDistance = clickRange;
        Vector3 position = transform.position;

        foreach (Button btn in buttons)
        {
            float distance = Vector3.Distance(position, btn.transform.position);
            if (distance < minDistance)
            {
                minDistance = distance;
                closest = btn;
            }
        }
        return closest;
    }
    public void PushButton()
    {
        Button closestButton = FindClosestButtonInRange();
        if (closestButton != null)
        {
            closestButton.onClick.Invoke();
            Debug.Log("Object clicked button: " + closestButton.name);
        }
        else
        {
            Debug.Log("No button within range!");
        }
    }
   */
}