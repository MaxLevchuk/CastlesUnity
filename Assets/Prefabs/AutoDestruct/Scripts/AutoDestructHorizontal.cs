using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructHorizontal : MonoBehaviour
{
    public int ScoreByDestroy;
    public GameObject TopPartPrefab;    
    public GameObject BottomPartPrefab;
   
    public float destructionForceThreshold = 10f;
    private bool isDestroyed = false;

    private BallCount ballCount;

    private void Start()
    {
        ballCount = FindObjectOfType<BallCount>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (isDestroyed) return;

        if (collision.rigidbody == null)
        {
            Debug.LogWarning("Colliding object does not haves a Rigidbody2D.");
            return;
        }

        float collisionForce = collision.relativeVelocity.magnitude * collision.rigidbody.mass;
        if (collisionForce > destructionForceThreshold)
        {
            DestroyObject();
        }
    }

    public void DestroyObject()
    {
        if (isDestroyed) return;

        ScoreManager.instance.AddPoint(ScoreByDestroy);
        isDestroyed = true;

  
        if (BottomPartPrefab != null)
        {
           
            GameObject BottomPart = Instantiate(BottomPartPrefab, transform.position, transform.rotation);
            BottomPart.transform.localScale = transform.localScale;

            Rigidbody2D originalRigidbody = GetComponent<Rigidbody2D>();
            Rigidbody2D[] leftRigidbodies = BottomPart.GetComponentsInChildren<Rigidbody2D>();

            foreach (var rb in leftRigidbodies)
            {
                rb.velocity = originalRigidbody.velocity;
                rb.angularVelocity = originalRigidbody.angularVelocity;
            }
        }
        else
        {
            Debug.LogWarning("BottomPart is not set in the inspector.");
        }

      
        if (TopPartPrefab != null)
        {
          
            GameObject TopPart = Instantiate(TopPartPrefab, transform.position, transform.rotation);
            TopPart.transform.localScale = transform.localScale;

            Rigidbody2D originalRigidbody = GetComponent<Rigidbody2D>();
            Rigidbody2D[] rightRigidbodies = TopPart.GetComponentsInChildren<Rigidbody2D>();

            foreach (var rb in rightRigidbodies)
            {
                rb.velocity = originalRigidbody.velocity;
                rb.angularVelocity = originalRigidbody.angularVelocity;
            }
        }
        else
        {
            Debug.LogWarning("TopPart is not set in the inspector.");
        }

        Destroy(gameObject);

        if (ballCount.GetBallCount() <= 0)
        {
            ballCount.NoBallsLeft();
        }
    }
}
