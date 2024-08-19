using System;
using UnityEngine;

public class DestructibleObject : MonoBehaviour
{
    public int ScoreByDestroy;
    public GameObject DestructibleObjectPrefab;
    public float destructionForceThreshold = 10f;
    private bool isDestroyed = false;

    private BallCount ballCount;

    private void Start()
    {
        ballCount = FindObjectOfType<BallCount>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroyed) return;

        if (collision.rigidbody == null)
        {
            Debug.LogWarning("Colliding object does not have a Rigidbody2D.");
            return;
        }

        float collisionForce = collision.relativeVelocity.magnitude * collision.rigidbody.mass;
        if (collisionForce > destructionForceThreshold)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        if (isDestroyed) return;

        ScoreManager.instance.AddPoint(ScoreByDestroy);
        isDestroyed = true;

        if (DestructibleObjectPrefab != null)
        {
            GameObject destroyedObject = Instantiate(DestructibleObjectPrefab, transform.position, transform.rotation);

            Rigidbody2D originalRigidbody = GetComponent<Rigidbody2D>();
            Rigidbody2D[] destroyedRigidbodies = destroyedObject.GetComponentsInChildren<Rigidbody2D>();

            foreach (var rb in destroyedRigidbodies)
            {
                rb.velocity = originalRigidbody.velocity;
                rb.angularVelocity = originalRigidbody.angularVelocity;
            }
        }
        else
        {
            Debug.LogWarning("DestroyedWallPrefab is not set in the inspector.");
        }

        Destroy(gameObject);

        if (ballCount.GetBallCount() <= 0)
        {
            ballCount.NoBallsLeft();
        }
    }
}