using UnityEngine;

public class Scr_Building : MonoBehaviour
{
    private Animator animator;
    public AnimationClip Building;
    public int startFrame;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play(Building.name);
    }

    void Update()
    {
        startFrame = GetCurrentFrame();
        // Example: Play the animation from the specified frame on update
        
    }

    int GetCurrentFrame()
    {
        if (animator != null && Building != null)
        {
            // Get the current state info of the animator
            AnimatorStateInfo stateInfo = animator.GetCurrentAnimatorStateInfo(0);

            // Get the normalized time (0 to 1) of the animation
            float normalizedTime = stateInfo.normalizedTime % 1; // Use modulo to handle looping animations

            // Calculate the total number of frames in the animation
            float totalFrames = Building.frameRate * Building.length;

            // Calculate the current frame based on normalized time
            int currentFrame = Mathf.FloorToInt(normalizedTime * totalFrames);

            return currentFrame;
        }

        return 0; // Default to frame 0 if animator or Building is null
    }
}