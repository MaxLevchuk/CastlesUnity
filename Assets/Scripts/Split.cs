using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : MonoBehaviour
{
    public GameObject smallBallPrefab;
    // Number of small balls to spawn
    public int smallBallCount = 3;
    // Time delay before transformation
    public float transformationDelay = 0.65f;
    // Angle to spread the small balls
    public float spreadAngle = 15f;

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
        hasCollided = true;
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
}
