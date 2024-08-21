using System.Collections;
using UnityEngine;

public class Portal : MonoBehaviour
{
    public Transform connectedPortal; // Connected portal
    public bool isBluePortal; // Flag to indicate blue or red portal
    public float teleportCooldown = 0.5f; // Delay between teleportations

    private bool canTeleport = true; // Can teleport flag

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

        // Disable teleportation through the connected portal
        Portal connectedPortalScript = connectedPortal.GetComponent<Portal>();
        connectedPortalScript.canTeleport = false;

        // Save the object's physical properties
        Rigidbody2D rb = ball.GetComponent<Rigidbody2D>();
        Vector2 velocity = rb.velocity;
        float angularVelocity = rb.angularVelocity;

        // Calculate the ball's offset relative to the current portal
        Vector3 relativePosition = ball.transform.position - transform.position;

        // Transform the velocity vector into the portal's local space
        Vector2 localVelocity = transform.InverseTransformDirection(velocity);

        // Create a new object at the connected portal's position with the calculated offset
        GameObject newBall = Instantiate(ball.gameObject, connectedPortal.position + relativePosition, ball.transform.rotation);

        // Transfer the physical properties to the new object
        Rigidbody2D newRb = newBall.GetComponent<Rigidbody2D>();

        // Apply the transformed velocity to the new ball, based on the orientation of the connected portal
        Vector2 portalVelocity = connectedPortal.TransformDirection(localVelocity);
        newRb.velocity = portalVelocity;
        newRb.angularVelocity = angularVelocity;

        // Destroy the old object
        Destroy(ball.gameObject);

        // Wait before allowing the next teleport
        yield return new WaitForSeconds(teleportCooldown);

        canTeleport = true;
        connectedPortalScript.canTeleport = true;
    }
}
