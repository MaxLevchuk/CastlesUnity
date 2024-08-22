using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform connectedPortal;
    public float teleportCooldown = 0.5f;

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

        yield return new WaitForSeconds(teleportCooldown);

        canTeleport = true;
        connectedPortalScript.canTeleport = true;


        Destroy(ball.gameObject, 1f);
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