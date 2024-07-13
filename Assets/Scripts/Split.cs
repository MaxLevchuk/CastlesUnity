using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public GameObject smallBallPrefab;
    // Number of small balls to spawn
    public int smallBallCount = 3;
    // Time delay before transformation
    public float transformationDelay = 2f;
    // Angle to spread the small balls
    public float spreadAngle = 15f;

    // Explosion properties
    public GameObject explosionEffectPrefab;
    public float explosionRadius = 5.0f;
    public float explosionForce = 1500.0f;
    public float effectDisplayTime = 3.0f;

    private Rigidbody2D rb;
    private bool isLaunched = false;
    private float launchTime;
    private bool hasCollided = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Replaced velocity overwrite
    public void MarkAsLaunched()
    {
        isLaunched = true;
        launchTime = Time.time;
    }

    private void Update()
    {
        if (isLaunched && Time.time - launchTime >= transformationDelay && !hasCollided)
        {
            TransformIntoSmallBalls();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;

            if (Time.time - launchTime < transformationDelay)
            {
                ExplodeBall();
            }
        }
    }

    private void TransformIntoSmallBalls()
    {
        Vector2 currentVelocity = rb.velocity;

        // Destroy the main ball
        Destroy(gameObject);

        // Spawn small balls
        for (int i = 0; i < smallBallCount; i++)
        {
            GameObject smallBall = Instantiate(smallBallPrefab, transform.position, Quaternion.identity);
            Rigidbody2D smallRb = smallBall.GetComponent<Rigidbody2D>();

            // Calculate the spread angle
            float angle = (i - (smallBallCount - 1) / 2f) * spreadAngle;
            Vector2 spreadDirection = Quaternion.Euler(0, 0, angle) * currentVelocity;

            smallRb.velocity = spreadDirection;
        }
    }

    // Explosion logic
    private void ExplodeBall()
    {
        if (explosionEffectPrefab != null)
        {
            GameObject effect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, effectDisplayTime);
        }

        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = rb.transform.position - transform.position;
                float distance = direction.magnitude;
                float explosionForceAdjusted = explosionForce / (distance + 1);
                rb.AddForce(direction.normalized * explosionForceAdjusted);
            }

            DestructibleWall destructibleWall = collider.GetComponent<DestructibleWall>();
            if (destructibleWall != null)
            {
                destructibleWall.DestroyWall();
            }
        }

        Destroy(gameObject);
    }
}
