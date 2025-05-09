using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlatformerLevelChanger : MonoBehaviour
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!collision.collider.CompareTag("Player"))
            return;

        PlayerControllerCSH playerController = collision.collider.GetComponent<PlayerControllerCSH>();

        if (playerController != null && playerController.HasHeadphones)
        {
            Level_Connection.activeConnection = connection;
            SceneManager.LoadScene(targetSceneName);
            // Optionally use a transition:
            // LoadNextScene();
        }
        else
        {
            Debug.Log("Player needs headphones to proceed.");
        }
    }

    public void LoadNextScene()
    {
        StartCoroutine(LoadScene(targetSceneName));
    }

    IEnumerator LoadScene(string targetSceneName)
    {
        if (animator != null)
        {
            animator.SetTrigger("Start");
            yield return new WaitForSeconds(transitionTime);
        }

        SceneManager.LoadScene(targetSceneName);
    }
}
