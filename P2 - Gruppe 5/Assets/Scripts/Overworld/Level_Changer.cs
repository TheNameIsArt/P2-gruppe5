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

    //public Animator animator;
    //public float transitionTime;

    private void Start()
    {
        if (connection == Level_Connection.activeConnection)
        {
            if (player != null && spawnPoint != null)
                player.transform.position = spawnPoint.position;
        }
    }

    private void OnCollisionEnter2D(Collision2D Player_test)
    {
        SetActiveConnection();
        SceneFader.Instance.FastFadeToScene(targetSceneName);
    }

    public void SetActiveConnection()
    {
        Level_Connection.activeConnection = connection;
    }

    public void sceneChanger()
    {
        SetActiveConnection();
        SceneFader.Instance.FadeToScene(targetSceneName);
    }

    public void PerformAction()
    {
        sceneChanger();
    }

    /*IEnumerator LoadScene(string targetSceneName)
    {
        animator.SetTrigger("Start");

        yield return new WaitForSeconds(transitionTime);

        SceneManager.LoadScene(targetSceneName);
    }*/
}
