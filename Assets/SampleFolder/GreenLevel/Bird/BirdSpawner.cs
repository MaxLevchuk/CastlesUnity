using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdSpawner : MonoBehaviour
{
    public GameObject birdPrefab; 
    public float spawnInterval = 10f; 

    private void Start()
    {
        StartCoroutine(SpawnBirdsCoroutine());
    }

    private IEnumerator SpawnBirdsCoroutine()
    {
        while (true)
        {
            int birdCount = Random.Range(3, 5); 

            for (int i = 0; i < birdCount; i++)
            {
               
                Instantiate(birdPrefab, new Vector2(transform.position.x + Random.Range(-3.7f, 3.7f), transform.position.y+Random.Range(-3.3f, 3.3f)), Quaternion.identity);
            }

            yield return new WaitForSeconds(spawnInterval); 
        }
    }
}
