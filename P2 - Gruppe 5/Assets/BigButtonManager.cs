using UnityEngine;
using UnityEngine.UI;  // For UI elements like Image if needed

public class YellowSpriteChecker : MonoBehaviour
{
    public SpriteRenderer[] targetSprites; // Assign 3 sprites in the Inspector
    public Color targetColor = Color.yellow; // The color to check for
    public Button winButton;

    void Update()
    {
        if (CheckAllYellow())
        {
            TriggerAction();
        }
    }

    bool CheckAllYellow()
    {
        foreach (SpriteRenderer sprite in targetSprites)
        {
            if (sprite.color != targetColor)
            {
                return false; // If any sprite is not yellow, return false
            }
        }
        return true; // All sprites are yellow
    }

    void TriggerAction()
    {
        Debug.Log("All sprites are yellow! Unnlocking end button");
        winButton.interactable = true;
    }

    public void OnWinButtonClick()
    {
        Debug.Log("YOU WIN THE ENTIRE GAME AND HAVE SET UP THE LIGHT SHOW!!!!");
    }
}