using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderScript : MonoBehaviour
{
    private string nextSceneName;
    private string previousSceneName;
    private void Start()
    {
       
        Time.timeScale = 0f;
        Animator animator = GetComponent<Animator>();
        animator.updateMode = AnimatorUpdateMode.UnscaledTime;
        nextSceneName = "Level" + PlayerPrefs.GetInt("LevenNumber");
        previousSceneName = SceneManager.GetActiveScene().name;

    }
    public void LoadNextScene()
    {
        //in

        if (!string.IsNullOrEmpty(previousSceneName))
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

