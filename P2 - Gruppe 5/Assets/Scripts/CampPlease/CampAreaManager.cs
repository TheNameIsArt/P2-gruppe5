using UnityEngine;

public class CampAreaManager : MonoBehaviour
{
    private string correctCampLetter;

    public void SetCorrectCampLetter(string letter)
    {
        correctCampLetter = letter;
        Debug.Log("Correct camp letter is: " + correctCampLetter);
    }

    // Individual methods for each button
    public void PlayerSelectsCampA() { PlayerSelectsCamp("A"); }
    public void PlayerSelectsCampB() { PlayerSelectsCamp("B"); }
    public void PlayerSelectsCampC() { PlayerSelectsCamp("C"); }
    public void PlayerSelectsCampD() { PlayerSelectsCamp("D"); }
    public void PlayerSelectsCampE() { PlayerSelectsCamp("E"); }
    public void PlayerSelectsCampF() { PlayerSelectsCamp("F"); }
    public void PlayerSelectsCampG() { PlayerSelectsCamp("G"); }
    public void PlayerSelectsCampH() { PlayerSelectsCamp("H"); }

    private void PlayerSelectsCamp(string selectedLetter)
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
