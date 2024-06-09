using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainDissapearing : MonoBehaviour
{
    public float disappearTime = 4f;
    private SpriteRenderer spriteRenderer;
    private bool isBreaking = false;
    private float breakStartTime;
    public float destructionForceThreshold = 10f;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        if (isBreaking)
        {
            float elapsedTime = Time.time - breakStartTime;
            float currentAlpha = Mathf.Lerp(1f, 0f, elapsedTime / disappearTime);

            Color newColor = spriteRenderer.color;
            newColor.a = currentAlpha;
            spriteRenderer.color = newColor;

            if (elapsedTime >= disappearTime)
            {
                Destroy(gameObject);
            }
        }   
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        float collisionForce = collision.relativeVelocity.magnitude * collision.rigidbody.mass;

        if (!isBreaking && collisionForce > destructionForceThreshold)
        {
           
            Joint2D[] joints = GetComponents<Joint2D>();
            foreach (Joint2D joint in joints)
            {
                Destroy(joint);
            }

            isBreaking = true;
            breakStartTime = Time.time;
        }
    }
}
