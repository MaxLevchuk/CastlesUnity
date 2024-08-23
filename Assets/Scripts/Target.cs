using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{

    public static Target Instance { get; private set; }
    public float rotationSpeed = 100f;
    // public string nextSceneName;
    
    private string previousSceneName;

    private void Awake()
    {
        Instance = this;
    }
    void Update()
    {   
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {

           string previousSceneName = SceneManager.GetActiveScene().name;
            PlayerPrefs.SetInt("LevenNumber", GetNextLevelNumber(previousSceneName));
            SceneManager.LoadSceneAsync("LevelLoaderScene", LoadSceneMode.Additive);
        }
    }
    public int GetNextLevelNumber(string input)
    {
        string prefix = "Level";
        if (input.StartsWith(prefix))
        {
            if (int.TryParse(input.Substring(prefix.Length), out int levelNumber)) // delete word "Level" and string to int
            {
                return levelNumber + 1;//next level
            }   
        }

        return 0;
    }



}

