using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentLevel : MonoBehaviour
{
    public static CurrentLevel Instance { get; private set; }
    public int LevelNumber = 1;


    private void Awake()
    {
        Instance = this;
    }

}
