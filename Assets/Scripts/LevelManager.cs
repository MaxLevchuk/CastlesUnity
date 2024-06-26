using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public BallCount ballCount;
    public int balls;
    void Start()
    {
        if (ballCount != null)
        {
            ballCount.SetBallCount(balls);
        }
        else
        {
            Debug.LogError("BallCount is not assigned in LevelManager");
        }
    }
}
