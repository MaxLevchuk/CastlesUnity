using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    // Логіка повороту патріклсів вираховується від залежності руху снаряду у напрямку порталу
    public Transform connectedPortal;
    public float teleportCooldown = 0.5f;
    public GameObject entryParticlePrefab;
    public GameObject exitParticlePrefab;
    private bool canTeleport = true;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (canTeleport && other.CompareTag("Ball"))
        {
            StartCoroutine(Teleport(other));
        }
    }
    private IEnumerator Teleport(Collider2D ball)
    {
        canTeleport = false;
        Portal connectedPortalScript = connectedPortal.GetComponent<Portal>();
        connectedPortalScript.canTeleport = false;
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        Vector2 velocity = rb.velocity;
        float angularVelocity = rb.angularVelocity;
        Vector3 relativePosition = ball.transform.position - transform.position;
        Vector2 localVelocity = transform.InverseTransformDirection(velocity);
        GameObject newBall = Instantiate(ball.gameObject, connectedPortal.position + relativePosition, ball.transform.rotation);
        Rigidbody2D newRb = newBall.GetComponent<Rigidbody2D>();
        Vector2 portalVelocity = connectedPortal.TransformDirection(localVelocity);
        newRb.velocity = portalVelocity;
        newRb.angularVelocity = angularVelocity;

        DisableAllComponents(ball);

        GameObject exitParticles = Instantiate(exitParticlePrefab, newBall.transform.position, Quaternion.identity);
        ParticleSystem exitParticleSystem = exitParticles.GetComponent<ParticleSystem>();
        if (exitParticleSystem != null)
        {
            exitParticleSystem.Play();
        }

        GameObject entryParticles = Instantiate(entryParticlePrefab, ball.transform.position, Quaternion.identity);
        ParticleSystem entryParticleSystem = entryParticles.GetComponent<ParticleSystem>();
        if (entryParticleSystem != null)
        {
            entryParticleSystem.Play();
        }

        StartCoroutine(FollowEntryBall(entryParticles, newBall.transform));
        StartCoroutine(FollowOutBall(exitParticles, newBall.transform));

        yield return new WaitForSeconds(teleportCooldown);

        canTeleport = true;
        connectedPortalScript.canTeleport = true;

        Destroy(ball.gameObject, 1f);
    }

    private IEnumerator FollowOutBall(GameObject particles, Transform target)
    {
        ParticleSystem ps = particles.GetComponent<ParticleSystem>();
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        Vector2 localVelocity = transform.InverseTransformDirection(rb.velocity);
        float angleOffset = 0f;
        if (localVelocity.x > 0)
        {
            angleOffset = -90f;
        }
        else if (localVelocity.x < 0)
        {
            angleOffset = 90f;
        }
        float portalRotation = transform.eulerAngles.z;
        particles.transform.rotation = Quaternion.Euler(0, 0, portalRotation + angleOffset);
        yield break;
    }

    private IEnumerator FollowEntryBall(GameObject particles, Transform target)
    {
        ParticleSystem ps = particles.GetComponent<ParticleSystem>();
        Rigidbody2D rb = target.GetComponent<Rigidbody2D>();
        Vector2 localVelocity = transform.InverseTransformDirection(rb.velocity);
        float angleOffset = 0f;
        if (localVelocity.x > 0)
        {
            angleOffset = 90f;
        }
        else if (localVelocity.x < 0)
        {
            angleOffset = -90f;
        }
        float portalRotation = transform.eulerAngles.z;
        particles.transform.rotation = Quaternion.Euler(0, 0, portalRotation + angleOffset);
        yield break;
    }


    private void DisableAllComponents(Collider2D ball)
    {
        foreach (var component in ball.GetComponents<MonoBehaviour>())
        {
            component.enabled = false;
        }
        ball.GetComponent<Collider2D>().enabled = false;
        ball.GetComponent<Rigidbody2D>().simulated = false;
        ball.GetComponent<SpriteRenderer>().enabled = false;
    }
}
