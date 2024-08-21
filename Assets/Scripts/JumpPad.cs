using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class JumpPad : MonoBehaviour
{
    Animator animator;
    SoundEffects soundEffects;
    void Start()
    {
        animator = GetComponent<Animator>();
        soundEffects = GetComponent<SoundEffects>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ball"))
        {
            animator.SetTrigger("Jump");
            soundEffects.PlaySound();
        }
    }
}
