using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CampAreaButton : MonoBehaviour
{
    TMP_Text buttonText;
    string buttonString;
    private string correctCampLetter;
    private ExpressionSelector selector;

    private void Start()
    {
        selector = GameObject.Find("ExpressionReader").GetComponent<ExpressionSelector>();
    }
    public void OnButtonClick()
    {
        
        correctCampLetter = selector.correctCampLetter;
        buttonText = GetComponentInChildren<TMP_Text>();
        buttonString = buttonText.text;
        //Debug.Log("buttonString er: " + buttonString + "og buttonText er: " + buttonText.text + "og correctCampLetter er: " + correctCampLetter);
        if (buttonString != null)
        {
            if (buttonString == correctCampLetter)
            {
                Debug.Log("Correct Camp Selected: " + buttonString);
            }
            else
            {
                Debug.Log("Wrong Camp! Try Again.");
            }
        }
    }
}
