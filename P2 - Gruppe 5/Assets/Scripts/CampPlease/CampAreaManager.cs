using UnityEngine;

public class CampAreaManager : MonoBehaviour
{
    // Stores the correct camp letter that the player needs to choose
    private string correctCampLetter;

    // Sets the correct camp letter (received from ExpressionSelector script)
    public void SetCorrectCampLetter(string letter)
    {
        correctCampLetter = letter;
        Debug.Log("Correct camp letter is: " + correctCampLetter);
    }

    // Method called when the player selects a camp option
    public void PlayerSelectsCamp(string selectedLetter)
    {
        Debug.Log("Button clicked: " + selectedLetter); // Logs the player's choice

        // Checks if the selected camp matches the correct one
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
