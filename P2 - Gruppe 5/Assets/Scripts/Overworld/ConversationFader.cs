using UnityEngine;

public class ConversationFader : MonoBehaviour
{
    public void FadeIn()
    {
        SceneFader.Instance.FadeIn();
    }
    public void FadeOut()
    {
        SceneFader.Instance.FadeOut();
    }
}
