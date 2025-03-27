using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level_Changer : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Level_Connection connection;
    [SerializeField] private string targetSceneName;
    [SerializeField] private Transform spawnPoint;

    public Animator animator;
    public float transitionTime;

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
        //LoadNextScene();
        SceneManager.LoadScene(targetSceneName);
    }
    public void LoadNextScene() 
    {
        StartCoroutine(LoadScene(targetSceneName));
    }

    IEnumerator LoadScene(string targetSceneName)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(targetSceneName);
    }
}
