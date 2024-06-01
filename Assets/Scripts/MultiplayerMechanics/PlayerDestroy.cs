using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDestroy : MonoBehaviour
{
   
    public GameObject DestroyedPlayerPrefab;
    public float destructionForceThreshold = 10f;

    private bool isDestroyed = false;

    private void Start()
    {

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
            DestroyWall();
        }
    }

    private void DestroyWall()
    {
        
        isDestroyed = true;

        if (DestroyedPlayerPrefab != null)
        {
            GameObject destroyedWall = Instantiate(DestroyedPlayerPrefab, transform.position, transform.rotation);

            Rigidbody2D originalRigidbody = GetComponent<Rigidbody2D>();
            Rigidbody2D[] destroyedRigidbodies = destroyedWall.GetComponentsInChildren<Rigidbody2D>();

            foreach (var rb in destroyedRigidbodies)
            {
                rb.velocity = originalRigidbody.velocity;
                rb.angularVelocity = originalRigidbody.angularVelocity;
            }
        }
        else
        {
            Debug.LogWarning("DestroyedPlayerPrefab is not set in the inspector.");
        }

        Destroy(gameObject);
    }
}
