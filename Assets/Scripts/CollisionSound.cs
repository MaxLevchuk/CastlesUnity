using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.VisualScripting.Member;

public class CollisionSound : MonoBehaviour
{
    public AudioSource src;
    public AudioClip[] SFX;
    private System.Random rand = new System.Random();
    void Start()
    {
        src = gameObject.AddComponent<AudioSource>();
        src.clip = SFX[rand.Next(0,4)];
        src.Play();
    }

    // Update is called once per frame

}
