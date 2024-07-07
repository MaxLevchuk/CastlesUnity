using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceExplode : MonoBehaviour
{
    public GameObject explosionEffectPrefab;
    public float explosionRadius = 5.0f;
    public float explosionForce = 1500.0f;
    // WIP: Number of bounces before exploding, duration and disappearing
    public int maxBounces = 3;
    public float effectDisplayTime = 3.0f;
    public float disappearTime = 8f;

    private int bounceCount = 0;
    private SpriteRenderer spriteRenderer;
    private float collisionTime;
    private bool isTouched = false;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if (isTouched)
        {
            float elapsedTime = Time.time - collisionTime;

            if (elapsedTime >= 7f && elapsedTime < disappearTime)
            {
                float fadeElapsedTime = elapsedTime - 7f;
                float currentAlpha = Mathf.Lerp(1f, 0f, fadeElapsedTime / (disappearTime - 7f));
                Color newColor = spriteRenderer.color;
                newColor.a = currentAlpha;
                spriteRenderer.color = newColor;
                gameObject.GetComponent<TrailRenderer>().enabled = false;
            }

            if (elapsedTime >= disappearTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            bounceCount++;
            isTouched = true;
            collisionTime = Time.time;

            if (bounceCount >= maxBounces)
            {
                Explode();
            }
        }
    }

    private void Explode()
    {
        // Create the explosion effect
        if (explosionEffectPrefab != null)
        {
            GameObject effect = Instantiate(explosionEffectPrefab, transform.position, Quaternion.identity);
            Destroy(effect, effectDisplayTime);
        }

        // Apply explosion force to nearby objects
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, explosionRadius);
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                Vector2 direction = rb.transform.position - transform.position;
                float distance = direction.magnitude;
                float explosionForceAdjusted = explosionForce / (distance + 1); // Reduce force with distance
                rb.AddForce(direction.normalized * explosionForceAdjusted);
            }

            // Destroy destructible objects
            DestructibleWall destructibleWall = collider.GetComponent<DestructibleWall>();
            if (destructibleWall != null)
            {
                destructibleWall.DestroyWall();
            }
        }

        // Destroy the small ball
        Destroy(gameObject);
    }

    // Draw explosion radius in the editor
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explosionRadius);
    }
}
