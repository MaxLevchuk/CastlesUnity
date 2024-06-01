using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffects : MonoBehaviour
{
    public AudioSource src;
    public AudioClip sfx1, sfx2, sfx3;


    public void WallDestroySound()
    {
        src.clip = sfx1;
    }
}
