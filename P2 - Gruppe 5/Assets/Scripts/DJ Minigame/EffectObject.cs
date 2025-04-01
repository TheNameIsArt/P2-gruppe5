using UnityEngine;

public class EffectObject : MonoBehaviour
{
    private float lifetime = 0.5f;
    void Start()
    {
        
    }

    void Update()
    {
        Destroy(gameObject, lifetime);
    }
}
