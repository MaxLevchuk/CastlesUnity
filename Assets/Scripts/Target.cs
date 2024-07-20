using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Target : MonoBehaviour
{
    public static Target Instance { get; private set; }
    public float rotationSpeed = 100f;
    public string nextSceneName;

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
            if (!string.IsNullOrEmpty(nextSceneName))
            {
                SceneManager.LoadScene(nextSceneName);
            }
            else
            {
                Debug.LogError("Next scene name is not set.");
            }
        }
    }
}

