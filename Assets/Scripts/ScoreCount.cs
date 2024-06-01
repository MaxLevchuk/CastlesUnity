using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCount : MonoBehaviour
{
    public Text scoreText; // Ссылка на компонент Text
    public int scoreCount;

    void Start()
    {
        // Здесь можно добавить проверку на наличие ссылки на scoreText
        if (scoreText == null)
        {
            Debug.LogWarning("ScoreText is not set in the inspector.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + scoreCount;
        }
    }
}
