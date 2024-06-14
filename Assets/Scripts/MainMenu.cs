using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadSceneAsync("Level4");

    }
    public void PlayMultiplayerGame()
    {
        SceneManager.LoadSceneAsync("Level2multi");

    }
    public void ExitGame()
    {
       Application.Quit();

    }

}
