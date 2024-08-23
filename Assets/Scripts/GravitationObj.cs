using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationObj : MonoBehaviour
{
    public float gravityStrength = 9.8f;
    public float gravityRadius = 5.0f;   
    public LayerMask affectedLayers;    
    public float maxGravityForce = 50f;  
    public float ballGravityMultiplier = 2f; 
    private HashSet<Rigidbody2D> affectedRigidbodies = new HashSet<Rigidbody2D>();

    void FixedUpdate()
    {
       
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, gravityRadius, affectedLayers);

      
        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.attachedRigidbody;
            if (rb != null && affectedRigidbodies.Contains(rb))
            {
                Vector2 direction = (transform.position - rb.transform.position);
                float distance = direction.magnitude;
                direction.Normalize();           
                float gravityForce = gravityStrength * rb.mass / Mathf.Pow(distance, 2);
                if (rb.CompareTag("Ball"))
                {          
                    gravityForce *= ballGravityMultiplier;
                    float maxGravityForceForBall = maxGravityForce * ballGravityMultiplier;
                    gravityForce = Mathf.Min(gravityForce, maxGravityForceForBall);
                }
                else
                {
                    gravityForce = Mathf.Min(gravityForce, maxGravityForce);
                }
                rb.AddForce(direction * gravityForce, ForceMode2D.Force);
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null)
        {
            affectedRigidbodies.Add(rb);
            rb.gravityScale = 0; 
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        Rigidbody2D rb = other.attachedRigidbody;
        if (rb != null && affectedRigidbodies.Contains(rb))
        {
            affectedRigidbodies.Remove(rb);
            rb.gravityScale = 1;
        }
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
    }
}
