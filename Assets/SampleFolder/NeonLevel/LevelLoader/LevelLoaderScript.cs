using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoaderScript : MonoBehaviour
{
    private string nextSceneName;
    private string previousSceneName;
    public Text levelText;
    private void Start()
    {
        Time.timeScale = 0f;
        Animator animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        nextSceneName = "Level" + PlayerPrefs.GetInt("LevelNumber");
        previousSceneName = SceneManager.GetActiveScene().name;
        levelText.text = "Level " + PlayerPrefs.GetInt("LevelNumber"); 
    }
    public void LoadNextScene()
    {
        //in

        if (!string.IsNullOrEmpty(nextSceneName))
        {

            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
        }
       


    }
    public void UploadScene()
    {
        //out
        if (!string.IsNullOrEmpty(previousSceneName))
        {
           
            SceneManager.UnloadSceneAsync(previousSceneName);
        }


    }
    public void DeleteSceneLoader()
    {
    
            SceneManager.UnloadSceneAsync("LevelLoaderScene");
        


    }

    public void PlayTime()
    {
        Time.timeScale = 1f;
    }
    public void StopTime()
    {
        Time.timeScale = 0f;

    }

}

