using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using LuminousBlocks.Utils;

public class Target : MonoBehaviour
{
    public float rotationSpeed = 100f;
    public GameObject TopPartPrefab;
    public GameObject BottomPartPrefab;
    private Transform parentObject;
    private bool isDestroyed = false;
    void Start()
    {  
        parentObject = transform.parent;
    }

    void Update()
    {
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            DestroyObject();

            if (parentObject != null && parentObject.childCount == 1) 
            {
                string previousSceneName = SceneManager.GetActiveScene().name;
                PlayerPrefs.SetInt("LevelNumber", Utils.GetNextLevelNumber(previousSceneName));
                SceneManager.LoadSceneAsync("LevelLoaderScene", LoadSceneMode.Additive);
                SaveCurrentLevel();
            }
        }
    }
    private void SaveCurrentLevel()
    {


        string previousSceneName = SceneManager.GetActiveScene().name;
        if (Utils.GetNextLevelNumber(previousSceneName) > CurrentLevel.Instance.LevelNumber)
        { PlayerPrefs.SetInt("CurrentLevel", Utils.GetNextLevelNumber(previousSceneName)); }
        
    }

    
    public void DestroyObject()
    {
        if (isDestroyed) return;

        isDestroyed = true;


        if (BottomPartPrefab != null)
        {


            GameObject BottomPart = Instantiate(BottomPartPrefab, transform.position, transform.rotation);
            BottomPart.transform.localScale = transform.localScale;

            Rigidbody2D originalRigidbody = GetComponent<Rigidbody2D>();
            Rigidbody2D[] leftRigidbodies = BottomPart.GetComponentsInChildren<Rigidbody2D>();

            foreach (var rb in leftRigidbodies)
            {
                rb.velocity = originalRigidbody.velocity;
                rb.angularVelocity = originalRigidbody.angularVelocity;
            }
        }
        else
        {
            Debug.LogWarning("BottomPart is not set in the inspector.");
        }


        if (TopPartPrefab != null)
        {

            GameObject TopPart = Instantiate(TopPartPrefab, transform.position, transform.rotation);
            TopPart.transform.localScale = transform.localScale;

            Rigidbody2D originalRigidbody = GetComponent<Rigidbody2D>();
            Rigidbody2D[] rightRigidbodies = TopPart.GetComponentsInChildren<Rigidbody2D>();

            foreach (var rb in rightRigidbodies)
            {
                rb.velocity = originalRigidbody.velocity;
                rb.angularVelocity = originalRigidbody.angularVelocity;
            }
        }
        else
        {
            Debug.LogWarning("TopPart is not set in the inspector.");
        }

        Destroy(gameObject);

        if (BallCount.Instance.GetBallCount() <= 0)
        {
            BallCount.Instance.NoBallsLeft();
        }
    }
}
