using UnityEngine;
using TMPro;

public class panelNumberScript : MonoBehaviour
{
    public PanelScript[] panelScripts; // Array to hold references to multiple PanelScript instances
    public TMP_Text panelNumberText; // Reference to the TMP text component

    void Start()
    {
        if (panelScripts == null || panelScripts.Length != 3)
        {
            Debug.LogError("Please assign exactly 3 PanelScript references!");
            return;
        }

        if (panelNumberText == null)
        {
            Debug.LogError("Please assign a TextMeshProUGUI component to display the panel number!");
            return;
        }

        // Subscribe to the OnSpriteIndexChanged event for each PanelScript
        for (int i = 0; i < panelScripts.Length; i++)
        {
            if (panelScripts[i] != null)
            {
                panelScripts[i].OnSpriteIndexChanged += UpdatePanelNumber;
            }
            else
            {
                Debug.LogError($"PanelScript at index {i} is not assigned!");
            }
        }

        // Calculate the initial panel number
        UpdatePanelNumber(0); // Trigger an initial calculation
    }

    private void UpdatePanelNumber(int _)
    {
        // Get the current indexes of all three panels
        int index1 = panelScripts[0].GetCurrentIndex();
        int index2 = panelScripts[1].GetCurrentIndex();
        int index3 = panelScripts[2].GetCurrentIndex();

        // Calculate the unique number for the combination
        int panelNumber = GetPanelNumber(index1, index2, index3);

        // Update the TMP text with the panel number, adding a newline
        panelNumberText.text = $"Page:\n{panelNumber}";

        // Log the result (optional)
        //Debug.Log($"Panel Number for indexes ({index1}, {index2}, {index3}): {panelNumber}");
    }

    private int GetPanelNumber(int index1, int index2, int index3)
    {
        // Map the combination of indexes to a unique number between 1 and 8
        return 1 + (index1 % 2) + (index2 % 2) * 2 + (index3 % 2) * 4;
    }
}
