using UnityEngine;
using UnityEngine.SceneManagement;

public class ContextZone : MonoBehaviour
{
private bool isActionPerformed = false;
    void Start()
    {
      if(isActionPerformed) return;  
    }

   private void PerformAction()
    { 
        isActionPerformed = true; 
    }
}
