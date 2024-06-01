using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDisappearing : MonoBehaviour
{
    public float disappearTime = 8f;
    private SpriteRenderer spriteRenderer;
    private float collisionTime;
    bool isTouched = false;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();    
    }

    void Update()
    {
        if (isTouched)
        {   
            float elapsedTime = Time.time - collisionTime;

            if (elapsedTime >= 7f && elapsedTime < disappearTime)
            {
                float fadeElapsedTime = elapsedTime - 7f; 
                float currentAlpha = Mathf.Lerp(1f, 0f, fadeElapsedTime / (disappearTime - 7f));
                Color newColor = spriteRenderer.color;
                newColor.a = currentAlpha;
                spriteRenderer.color = newColor;
                gameObject.GetComponent<TrailRenderer>().enabled = false;
            }

            if (elapsedTime >= disappearTime)
            {
                Destroy(gameObject);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision != null)
        {
            isTouched = true;
            collisionTime = Time.time;
        }
    }
}
