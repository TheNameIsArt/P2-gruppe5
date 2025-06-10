using UnityEngine;
using UnityEngine.SceneManagement;

public class PersistentUIController : MonoBehaviour
{
    [SerializeField] private GameObject scene1Image; // assign the image in the Inspector
    [SerializeField] private string sceneToShowIn = "Scene1";

    void Awake()
    {
        DontDestroyOnLoad(gameObject); // if this controller is also persistent
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene1Image != null)
            scene1Image.SetActive(scene.name == sceneToShowIn);
    }
}
