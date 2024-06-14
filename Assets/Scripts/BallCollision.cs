using UnityEngine;

public class BallCollision : MonoBehaviour
{
    public GameObject DestroyedBallPrefab;
    public float explosionForce = 500f;
    public float randomRadius = 5f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            DestroyBall(collision.gameObject, collision.relativeVelocity);
        }
    }

    private void DestroyBall(GameObject ball, Vector2 collisionDirection)
    {
        Vector3 position = ball.transform.position;
        Quaternion rotation = ball.transform.rotation;

       
        GameObject destroyedBall = Instantiate(DestroyedBallPrefab, position, rotation);

        Rigidbody2D[] destroyedRigidbodies = destroyedBall.GetComponentsInChildren<Rigidbody2D>();

     
        Vector2 explosionDirection = -collisionDirection.normalized; //в обратну сторону 

        
        foreach (var rb in destroyedRigidbodies)
        {
            
            Vector2 randomOffset = Random.insideUnitCircle * randomRadius; 
            Vector2 finalDirection = (explosionDirection + randomOffset).normalized;
            rb.AddForce(finalDirection * explosionForce);
        }

       
        Destroy(ball);
    }
}
