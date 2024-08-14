using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudSpawner : MonoBehaviour
{
    public GameObject[] cloudPrefab;
    public float spawnInterval = 10f;

    private void Start()
    {
        StartCoroutine(SpawnCloudsCoroutine());
    }

    private IEnumerator SpawnCloudsCoroutine()
    {
        while (true)
        {
            Instantiate(cloudPrefab[Random.Range(0, cloudPrefab.Length)],
                        new Vector2(transform.position.x + Random.Range(-2.7f, 2.7f),
                        transform.position.y + Random.Range(-2f, 2f)),
                        Quaternion.identity);

            yield return new WaitForSeconds(spawnInterval);
        }
    }
}
