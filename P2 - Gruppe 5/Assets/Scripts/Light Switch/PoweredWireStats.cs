using UnityEngine;

public class PoweredWireStats : MonoBehaviour
{
    public enum Color { blue, red, yellow, green};
    public bool movable = false;
    public bool moving = false;
    public Vector3 startPosition;
    public Color objectColor;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
