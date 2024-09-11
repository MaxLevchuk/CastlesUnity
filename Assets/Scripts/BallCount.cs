using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class BallCount : MonoBehaviour
{
    public static BallCount Instance;
    public bool isTutorial = false; 
    public Image ballImagePrefab; 
    private List<Image> ballImages = new List<Image>();
    private int ballCount;
    public LevelManager levelManager;
    private float delayTime = 6f;
    private Coroutine loadSceneCoroutine;

    private void Awake()
    {
        Instance = this;
    }
    public void SetBallCount(int count)
    {
        ballCount = count;
        ClearBalls();
        SpawnBalls();
    }

    private void ClearBalls()
    {
        foreach (Image ballImage in ballImages)
        {
            Destroy(ballImage.gameObject);
        }
        ballImages.Clear();
    }

    public void SpawnBalls()
    {
        for (int i = 0; i < ballCount; i++)
        {
            if(isTutorial == false)
            {
                Image newBallImage = Instantiate(ballImagePrefab, transform);
                ballImages.Add(newBallImage);
            }
            else
            {             
                ballImages.Add(ballImagePrefab);
            }


        }

    }

    public void RemoveBall()
    {
        if (ballImages.Count > 0)
        {
            Image ballToRemove = ballImages[ballImages.Count - 1];
            ballImages.RemoveAt(ballImages.Count - 1);
            Destroy(ballToRemove.gameObject);
        }

        if (ballImages.Count == 0)
        {
            NoBallsLeft();
        }
    }

    private IEnumerator LoadNextSceneAfterDelay()
    {
        float elapsedTime = 0f;

        while (elapsedTime < delayTime)
        {
            yield return null; // Wait for the next frame
            elapsedTime += Time.deltaTime;
        }

        string currentSceneName = SceneManager.GetActiveScene().name;
        int currentLevelNumber = int.Parse(currentSceneName.Substring(5));
        PlayerPrefs.SetInt("LevelNumber", currentLevelNumber);
        SceneManager.LoadSceneAsync("LevelLoaderScene", LoadSceneMode.Additive);

    
    }

    public void NoBallsLeft()
    {
        if (loadSceneCoroutine != null)
        {
            StopCoroutine(loadSceneCoroutine);
        }
        loadSceneCoroutine = StartCoroutine(LoadNextSceneAfterDelay());
    }

    public int GetBallCount()
    {
        return ballImages.Count;
    }
}
