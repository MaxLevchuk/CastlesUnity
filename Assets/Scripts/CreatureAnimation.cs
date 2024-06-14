using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureAnimation : MonoBehaviour
{
    public string targetTag = "Ball"; 
    public float activationDistance = 5.0f;
    public float flyingSpeed = 1.0f;
    public float randomDirectionRange = 1.0f;
    public float swayAmplitude = 0.5f;
    public float swayFrequency = 1.0f;
    private Rigidbody2D rb;
    private BoxCollider2D bx2d;
    public float velocityThreshold = 0.1f;

    private Animator animator;
    private bool isFlying = false;
    private Vector2 randomDirection;
    private float startTime;
    public float minIdleSpeed = 0.5f;
    public float maxIdleSpeed = 1.5f;

    void Start()
    {
        bx2d = GetComponent<BoxCollider2D>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        float idleSpeed = Random.Range(minIdleSpeed, maxIdleSpeed);
        animator.SetFloat("IdleSpeed", idleSpeed);
        if (animator == null)
        {
            Debug.LogError("Animator component is missing on this game object.");
        }
    }

    void Update()
    {
        if (!isFlying)
        {
            GameObject[] targetObjects = GameObject.FindGameObjectsWithTag(targetTag);
            if (targetObjects.Length > 0)
            {
                
                GameObject nearestTarget = null;
                float minDistance = Mathf.Infinity;

                foreach (GameObject target in targetObjects)
                {
                    float distance = Vector2.Distance(transform.position, target.transform.position);
                    if (distance < minDistance)
                    {
                        minDistance = distance;
                        nearestTarget = target;
                    }
                }
           
                if (nearestTarget != null && minDistance <= activationDistance)
                {
                    StartFlying();
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (!isFlying && rb.velocity.y < -velocityThreshold)
        {
            StartFlying();
        }
        if (isFlying)
        {
           
            float sway = swayAmplitude * Mathf.Sin((Time.time - startTime) * swayFrequency);
            
            rb.velocity = new Vector2(randomDirection.x + sway, randomDirection.y) * flyingSpeed;
        }

        if (transform.position.y > 20)
        {
            Destroy(gameObject);
        }
   
    }

    private void StartFlying()
    {
        animator.SetBool("Flying", true);
        isFlying = true;
        startTime = Time.time;
  
        randomDirection = new Vector2(Random.Range(-randomDirectionRange, randomDirectionRange), 1.0f).normalized;
        rb.isKinematic = true;
        bx2d.enabled = false;
    }
}
