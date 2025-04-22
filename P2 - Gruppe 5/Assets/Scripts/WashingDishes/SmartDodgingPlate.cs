using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SmartDodgingPlate : MonoBehaviour
{
    [Header("Dodge Settings")]
    public float dodgeDistance = 1f;
    public float dodgeDuration = 0.6f;
    public float dodgeCooldown = 1f;
    private bool isDodging = false;
    private float lastDodgeTime = -10f;

    [Header("Idle Movement Settings")]
    public float idleAmplitude = 0.1f;
    public float idleSpeed = 1f;
    private Vector3 idleDirection;
    private Vector3 idleOrigin;
    private float idleTimer = 0f;

    [Header("Detection Settings")]
    public float detectionRadius = 3f;

    void Start()
    {
        idleOrigin = transform.position;
        idleDirection = new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;
        idleSpeed = Random.Range(0.5f, 1.5f);
        idleAmplitude = Random.Range(0.05f, 0.15f);
    }

    void Update()
    {
        if (!isDodging)
        {
            IdleMovement();
            ScanForProjectiles();
        }
    }

    void IdleMovement()
    {
        idleTimer += Time.deltaTime;
        float offset = Mathf.Sin(idleTimer * idleSpeed) * idleAmplitude;
        transform.position = idleOrigin + idleDirection * offset;
    }

    void ScanForProjectiles()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, detectionRadius);
        List<Collider2D> projectiles = new List<Collider2D>();

        foreach (var hit in hits)
        {
            if (hit.CompareTag("Projectile"))
            {
                projectiles.Add(hit);
            }
        }

        if (projectiles.Count > 0 && Time.time > lastDodgeTime + dodgeCooldown)
        {
            Vector2 safestDirection = CalculateSafestDodgeDirection(projectiles);
            StartCoroutine(DodgeRoutine(safestDirection));
        }
    }

    Vector2 CalculateSafestDodgeDirection(List<Collider2D> threats)
    {
        Vector2[] directions = new Vector2[]
        {
        Vector2.up,
        Vector2.down,
        Vector2.left,
        Vector2.right,
        new Vector2(1, 1).normalized,
        new Vector2(-1, 1).normalized,
        new Vector2(1, -1).normalized,
        new Vector2(-1, -1).normalized
        };

        float bestScore = float.MaxValue;
        Vector2 bestDirection = Vector2.zero;

        foreach (Vector2 dir in directions)
        {
            Vector2 testPosition = (Vector2)transform.position + dir * dodgeDistance;
            float totalThreat = 0f;

            foreach (var threat in threats)
            {
                float dist = Vector2.Distance(testPosition, threat.transform.position);
                totalThreat += 1f / Mathf.Max(dist, 0.1f); // avoid divide by zero, penalize closeness
            }

            if (totalThreat < bestScore)
            {
                bestScore = totalThreat;
                bestDirection = dir;
            }
        }

        return bestDirection == Vector2.zero ? Random.insideUnitCircle.normalized : bestDirection;
    }


    IEnumerator DodgeRoutine(Vector2 dodgeDirection)
    {
        isDodging = true;

        Vector3 start = transform.position;
        Vector3 end = start + (Vector3)(dodgeDirection * dodgeDistance);
        Quaternion startRot = transform.rotation;
        Quaternion endRot = Quaternion.Euler(0, 0, Random.Range(-20f, 20f));

        float t = 0f;

        while (t < dodgeDuration)
        {
            float progress = t / dodgeDuration;
            transform.position = Vector3.Lerp(start, end, progress);
            transform.rotation = Quaternion.Lerp(startRot, endRot, progress);
            t += Time.deltaTime;
            yield return null;
        }

        transform.position = end;
        transform.rotation = startRot;

        lastDodgeTime = Time.time;
        idleOrigin = transform.position;
        isDodging = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }

    // 💥 Detects when a projectile hits the plate and destroys it
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Projectile"))
        {
            Destroy(other.gameObject);
        }
    }
}


