using UnityEngine;

public class BallParticleCollision : MonoBehaviour
{
    public ParticleSystem hitParticles;

    private void Start()
    {
        if (hitParticles != null)
        {
            hitParticles.Stop();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // Collision check-up
        Debug.Log("Collision detected with " + collision.gameObject.name);
        Debug.Log("Collision point: " + collision.contacts[0].point);
        Debug.Log("Collision strength: " + collision.relativeVelocity.magnitude);

        if (hitParticles != null)
        {
            PlayParticleEffect(collision.contacts[0].point, collision.relativeVelocity.magnitude);
        }
    }

    private void PlayParticleEffect(Vector2 collisionPoint, float collisionStrength)
    {
        // Particle movement check-up
        hitParticles.transform.position = collisionPoint;
        Debug.Log("Particle system moved to: " + hitParticles.transform.position);

        if (collisionStrength < 1f)
        {
            // Low impact, few particles
            hitParticles.Emit(2);
            Debug.Log("Playing low impact burst.");
        }
        else if (collisionStrength < 5f)
        {
            // Moderate impact, more particles
            hitParticles.Emit(7);
            Debug.Log("Playing moderate impact burst.");
        }
        else
        {
            // High impact, many particles
            hitParticles.Emit(15);
            Debug.Log("Playing high impact burst.");
        }

        hitParticles.Play();
    }
}

