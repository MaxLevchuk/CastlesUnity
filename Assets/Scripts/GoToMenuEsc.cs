using UnityEngine;
using UnityEngine.SceneManagement;

public class GoToMenuEsc : MonoBehaviour
{
    public string sceneName; // The name of the scene you want to switch to

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}
