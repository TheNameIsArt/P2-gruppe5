using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Changer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;

    [SerializeField]
    private Level_Connection connection;

    [SerializeField]
    private string targetSceneName;

    [SerializeField]
    private Transform spawnPoint;

    private void Start()
    {
        if (connection == Level_Connection.activeConnection) 
        {
            player.transform.position = spawnPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D Player_test)
    {
        Level_Connection.activeConnection = connection;
        SceneManager.LoadScene(targetSceneName);
    }
}
