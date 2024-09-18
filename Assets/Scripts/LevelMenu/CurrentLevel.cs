using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class CurrentLevel : MonoBehaviour
{

    public static CurrentLevel Instance { get; private set; }
    public int LevelNumber = 1;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject);
        }
        if (PlayerPrefs.HasKey("CurrentLevel"))
        {
            LevelNumber = PlayerPrefs.GetInt("CurrentLevel");
        }
    }


}
