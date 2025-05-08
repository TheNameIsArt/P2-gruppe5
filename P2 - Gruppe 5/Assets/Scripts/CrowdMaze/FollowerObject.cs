using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerPathRecorder : MonoBehaviour
{
    public GameObject follower;
    public float followerSpeed = 5f;

    private List<Vector3> pathPositions = new List<Vector3>();
    private bool recording = true;
    private bool following = false;
    private int followIndex = 0;

    private void Update()
    {
        if (recording)
        {
            // Store positions, but only if different from last
            if (pathPositions.Count == 0 || Vector3.Distance(transform.position, pathPositions[^1]) > 0.1f)
            {
                pathPositions.Add(transform.position);
            }
        }
        else if (following && pathPositions.Count > 0)
        {
            // Move follower along the path
            if (followIndex < pathPositions.Count)
            {
                Vector3 target = pathPositions[followIndex];
                follower.transform.position = Vector3.MoveTowards(follower.transform.position, target, followerSpeed * Time.deltaTime);

                if (Vector3.Distance(follower.transform.position, target) < 0.1f)
                {
                    followIndex++;
                }
            }
        }
    }

    public void OnFollow(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            recording = false;
            following = true;
            follower.transform.position = pathPositions[0];
            followIndex = 1;
        }
    }
}