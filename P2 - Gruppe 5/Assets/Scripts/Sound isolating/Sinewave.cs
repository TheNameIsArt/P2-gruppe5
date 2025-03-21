//from video at https://youtu.be/6C1NPy321Nk
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Sinewave : MonoBehaviour
{
    public LineRenderer myLineRenderer;
    public int points;
    public float amplitude = 1;
    public float frequency = 1;
    public Vector2 xLimits = new Vector2(0, 1);
    public float movementSpeed = 1;
    [Range(0, 2 * Mathf.PI)]
    public float radians;
    void Start()
    {
        myLineRenderer = GetComponent<LineRenderer>();
        float randomValue1 = Random.Range(0.3f, 1.5f);
        float randomValue2 = Random.Range(0.3f, 1f);
        Debug.Log("Random Floats: " + randomValue1 + " and " + randomValue2);

        frequency = randomValue1;
        amplitude = randomValue2;
    }

    void Draw()
    {
        float xStart = xLimits.x;
        float Tau = 2 * Mathf.PI;
        float xFinish = xLimits.y;

        myLineRenderer.positionCount = points;
        for (int currentPoint = 0; currentPoint < points; currentPoint++)
        {
            float progress = (float)currentPoint / (points - 1);
            float x = Mathf.Lerp(xStart, xFinish, progress);
            float y = amplitude * Mathf.Sin((Tau * frequency * x) + (Time.timeSinceLevelLoad * movementSpeed));
            myLineRenderer.SetPosition(currentPoint, new Vector3(x, y, 0));
        }
    }

    public void Reroll()
    {
        float randomValue1 = Random.Range(0.3f, 1.5f);
        float randomValue2 = Random.Range(0.3f, 1f);
        Debug.Log("Random Floats: " + randomValue1 + " and " + randomValue2);

        frequency = randomValue1;
        amplitude = randomValue2;
    }

    void Update()
    {
        Draw();
        if (Input.GetKey(KeyCode.Space))
        {
            Reroll();
        }
    }
}
