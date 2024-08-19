using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{
    // Private variables
    [SerializeField] private float delay = 1.0f;
    [SerializeField] private float radius = 5.0f;
    [SerializeField] private float force = 1500.0f;
    [SerializeField] private bool explodeOnCollision = false;
    [SerializeField] private GameObject effectsPrefab;
    [SerializeField] private float effectDisplayTime = 3.0f;

    private float delayTimer;
    private bool hasCollided = false;

    // Unity methods
    private void Awake()
    {
        delayTimer = 0.0f;
    }

    private void Update()
    {
        if (hasCollided)
        {
            delayTimer += Time.deltaTime;

            if (delayTimer >= delay)
            {
                DoExplosion();
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (!hasCollided)
        {
            hasCollided = true;

            if (explodeOnCollision)
            {
                DoExplosion();
                Destroy(gameObject);
            }
        }
    }

    // Helper methods
    private void DoExplosion()
    {
        HandleEffects();
        HandleDestruction();
    }

    private void HandleEffects()
    {
        if (effectsPrefab != null)
        {
            GameObject effect = Instantiate(effectsPrefab, transform.position, Quaternion.identity);
            Destroy(effect, effectDisplayTime);
        }
    }

    private void HandleDestruction()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, radius);
        List<Rigidbody2D> affectedRigidbodies = new List<Rigidbody2D>();

        // Destroy destructible objects and collect rigidbodies
        foreach (Collider2D collider in colliders)
        {
            DestructibleObject destructibleWall = collider.GetComponent<DestructibleObject>();
            if (destructibleWall != null)
            {
                destructibleWall.DestroyObject(); // Call destruction method directly
            }
        }

        // Recalculate colliders to include new fragments
        colliders = Physics2D.OverlapCircleAll(transform.position, radius);

        // Apply explosion force to all affected objects
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rigidbody = collider.GetComponent<Rigidbody2D>();

            if (rigidbody != null)
            {
                Vector2 direction = rigidbody.transform.position - transform.position;
                float distance = direction.magnitude;
                float explosionForce = force / (distance + 1); // Reduce force with distance
                rigidbody.AddForce(direction.normalized * explosionForce);
            }
        }
    }

    // Draw explosion radius in editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radius);
    }
}

