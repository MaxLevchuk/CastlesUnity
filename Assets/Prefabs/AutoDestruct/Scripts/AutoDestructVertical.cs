using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoDestructVertical : MonoBehaviour
{
    public int ScoreByDestroy;
    public GameObject RightPartPrefab;
    public GameObject LeftPartPrefab;
   
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

  
        if (RightPartPrefab != null)
        {
           
            GameObject RightPart = Instantiate(RightPartPrefab, transform.position, transform.rotation);
            RightPart.transform.localScale = transform.localScale;

            Rigidbody2D originalRigidbody = GetComponent<Rigidbody2D>();
            Rigidbody2D[] leftRigidbodies = RightPart.GetComponentsInChildren<Rigidbody2D>();

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

      
        if (LeftPartPrefab != null)
        {
          
            GameObject LeftPart = Instantiate(LeftPartPrefab, transform.position, transform.rotation);
            LeftPart.transform.localScale = transform.localScale;

            Rigidbody2D originalRigidbody = GetComponent<Rigidbody2D>();
            Rigidbody2D[] rightRigidbodies = LeftPart.GetComponentsInChildren<Rigidbody2D>();

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


        if (BallCount.Instance.GetBallCount() <= 0)
        {
            BallCount.Instance.NoBallsLeft();
        }
    }
}
