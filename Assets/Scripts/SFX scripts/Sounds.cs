using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sounds : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx1, sfx2, sfx3, sfx4;
    private System.Random rand = new System.Random();
    public float volume = 1.0f;

    private void Start()
    {
        src = gameObject.AddComponent<AudioSource>();
        PlaySound();
        src.volume = volume;
    }

    private void PlaySound()
    {
        AudioClip selectedClip = null;

        switch (rand.Next(0, 4)) // Дефолтний рандом зі всічом для різних звуків залочених у інспекторі префаба
        {
            case 0:
                selectedClip = sfx1;
                break;
            case 1:
                selectedClip = sfx2;
                break;
            case 2:
                selectedClip = sfx3;
                break;
            case 3:
                selectedClip = sfx4;
                break;
            default:
                break;
        }

        if (selectedClip != null)
        {
            src.clip = selectedClip;

            if (!src.enabled)
            {
                src.enabled = true;
            }

       
            float pitch = (float)rand.NextDouble() * 5f + 4f; // рандомна тональність звуку 0.5 до 2.5
            
            src.pitch = pitch;

            src.Play();
        }
    }
}
