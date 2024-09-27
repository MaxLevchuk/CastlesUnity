using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionParticles : MonoBehaviour
{
    public ParticleSystem collisionParticlesPrefab;
    public float minForceForParticles = 0.8f;
    public float forceMultiplier = 1.0f;
    public int minParticles = 3;
    public int maxParticles = 8;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Check for breakable tag
        if (collision.gameObject.CompareTag("Breakable"))
        {
            Vector2 contactPoint = collision.contacts[0].point;

            float collisionForce = collision.relativeVelocity.magnitude;

            // Trigger if collision force is significant
            if (collisionForce > minForceForParticles)
            {
                ParticleSystem particles = Instantiate(collisionParticlesPrefab, contactPoint, Quaternion.identity);

                // Adjust particle emission count based on collision force
                var emissionModule = particles.emission;
                int particleCount = (int)Mathf.Lerp(minParticles, maxParticles, collisionForce / 10.0f); // Scale between min and max
                emissionModule.SetBursts(new ParticleSystem.Burst[] { new ParticleSystem.Burst(0, particleCount) });

                // Modify the particle system's velocity based on collision force
                var mainModule = particles.main;
                mainModule.startSpeed = collisionForce * forceMultiplier;

                particles.Play();

                Destroy(particles.gameObject, particles.main.duration + particles.main.startLifetime.constantMax);
            }
        }
    }
}
