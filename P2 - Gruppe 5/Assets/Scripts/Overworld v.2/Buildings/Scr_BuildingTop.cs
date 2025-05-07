using UnityEngine;

public class Scr_BuildingTop : MonoBehaviour
{
    public GameObject underSprite;
    public AnimationClip building;

    private Animator animator; 
    private int startFrame; // The frame to start the animation from
    

    private void Awake()
    {
        animator = GetComponent<Animator>();
        animator.Play(building.name);
    }

    void Update()
    {
        //startFrame = underSprite.GetComponent<Scr_Building>.startframe;
        // Example: Play the animation from the specified frame on update
        PlayAnimationFromFrame(startFrame);
    }

    void PlayAnimationFromFrame(int frame)
    {
        if (building != null)
        {
            // Calculate normalized time
            float totalFrames = building.frameRate * building.length;
            float normalizedTime = frame / totalFrames;

            // Play the animation from the specified normalized time
            animator.Play(building.name, 0, normalizedTime);
        }
    }
}