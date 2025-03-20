using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Lvl_Transporter : MonoBehaviour
{
    public bool NextLvl;


 

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (NextLvl)
        {
            if (collision.CompareTag("Player"))
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1);
            }
        }
        else 
        {
            if (collision.CompareTag("Player"))
            {
                SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex - 1);
            }
        }
       
    }

}
