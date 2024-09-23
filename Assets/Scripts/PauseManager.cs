using LuminousBlocks.Utils;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pauseMenuUI;
    public static bool isPaused = false;

    void Start()
    {
       
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
    }

    public void TogglePause()
    {
        if (isPaused)
        {
            Resume();
        }
        else
        {
            Pause();
        }
    }

    void Pause()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(true);
        }
        Time.timeScale = 0f; 
        Cursor.visible = true; 
        isPaused = true;
    }
    public void MainMenuButton()
    {
        isPaused = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
    public void Resume()
    {
        if (pauseMenuUI != null)
        {
            pauseMenuUI.SetActive(false);
        }
        Time.timeScale = 1f;
        
        isPaused = false;
    }

    public void Restart()
    {
        if (!IsSceneAlreadyLoaded("LevelLoaderScene"))
        {
            string previousSceneName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetInt("LevelNumber", Utils.GetActiveLevelNumber(previousSceneName));
            SceneManager.LoadSceneAsync("LevelLoaderScene", LoadSceneMode.Additive);
        }
        else
        {
            Debug.Log("Scene is loaded. Wait");
        }    
    }

    public bool IsPaused()
    {
        return isPaused;
    }


    bool IsSceneAlreadyLoaded(string sceneName)
    {
        Scene scene = SceneManager.GetSceneByName(sceneName);
        return scene.isLoaded;
    }

}
