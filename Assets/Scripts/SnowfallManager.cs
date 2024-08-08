using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnowfallManager : MonoBehaviour
{
    public GameObject[] snowflakePrefabs;
    // Time interval between spawns
    public float spawnRate = 0.5f;
    // X range for spawning snowflakes and speed
    public Vector2 spawnRangeX = new Vector2(-20f, 20f);
    public float fallSpeed = 1f;

    private float nextSpawnTime;

    void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnSnowflake();
            nextSpawnTime = Time.time + spawnRate;
        }
    }

    void SpawnSnowflake()
    {
        float spawnX = Random.Range(spawnRangeX.x, spawnRangeX.y);
        Vector3 spawnPosition = new Vector3(spawnX, transform.position.y, 0);

        // Random snowflake prefab selection
        int randomIndex = Random.Range(0, snowflakePrefabs.Length);
        GameObject selectedSnowflakePrefab = snowflakePrefabs[randomIndex];

        GameObject snowflake = Instantiate(selectedSnowflakePrefab, spawnPosition, Quaternion.identity);

        Rigidbody2D rb = snowflake.GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(0, -fallSpeed);
    }
}

