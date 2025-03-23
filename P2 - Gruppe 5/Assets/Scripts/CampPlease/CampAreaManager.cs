using UnityEngine;

public class CampAreaManager : MonoBehaviour
{
    private string correctCampLetter;

    public void SetCorrectCampLetter(string letter)
    {
        correctCampLetter = letter;
        Debug.Log("Correct camp letter is: " + correctCampLetter);
    }

    public void PlayerSelectsCamp(string selectedLetter)
    {
        Debug.Log("Button clicked: " + selectedLetter);

        if (selectedLetter == correctCampLetter)
        {
            Debug.Log("Correct Camp Selected: " + selectedLetter);
        }
        else
        {
            Debug.Log("Wrong Camp! Try Again.");
        }
    }
}
