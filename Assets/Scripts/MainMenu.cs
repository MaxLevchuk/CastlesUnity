using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("LevelMenu");

    }
    public void LoadOptions()
    {
        SceneManager.LoadSceneAsync("Settings");

    }
    public void PlayMultiplayerGame()
    {
        SceneManager.LoadSceneAsync("Level2multi");

    }
    public void ExitGame()
    {
       Application.Quit();
            
    }

    public void BackButton()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

}
