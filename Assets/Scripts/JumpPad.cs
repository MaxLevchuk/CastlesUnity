using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpPad : MonoBehaviour
{
    Animator animator;
    SoundEffects soundEffects;

    public float trajectoryCorrectionForce = 5f;
    public float forceMultiplier = 1f; 

    void Start()
    {
        animator = GetComponent<Animator>();
        soundEffects = GetComponent<SoundEffects>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            animator.SetTrigger("Jump");
            soundEffects.PlaySound();
            Rigidbody2D ballRigidbody = collision.gameObject.GetComponent<Rigidbody2D>();

            if (ballRigidbody != null)
            {
               
                Vector2 currentVelocity = ballRigidbody.velocity;

               
                Vector2 jumpPadDirection = transform.up;
             
                Vector2 correctedVelocity = currentVelocity + jumpPadDirection * trajectoryCorrectionForce;

              
                correctedVelocity = correctedVelocity.normalized * currentVelocity.magnitude;

               
                correctedVelocity *= forceMultiplier;

              
                ballRigidbody.velocity = correctedVelocity;
            }
        }
    }
}
