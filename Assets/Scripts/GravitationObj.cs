using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitationObj : MonoBehaviour
{
    public float gravityStrength = 9.8f; 
    public float gravityRadius = 5.0f;   
    public LayerMask affectedLayers;     

    void FixedUpdate()
    {
       
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, gravityRadius, affectedLayers);

        foreach (Collider2D collider in colliders)
        {
            Rigidbody2D rb = collider.attachedRigidbody;
            if (rb != null)
            {
               
                Vector2 direction = (transform.position - rb.transform.position).normalized;

              
                rb.AddForce(direction * gravityStrength * rb.mass);
            }
        }
    }

  
    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, gravityRadius);
    }
}
