using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class Settings : MonoBehaviour
{
    public AudioMixer audioMixer;
    private ColorAdjustments colorAdjustments;
    public Volume GlobalVolume;
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("General Volume", volume);
    }

    public void SetBrightness(float brightness)
    {
        if (GlobalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = brightness;
        }
        else
        {
            Debug.LogWarning("ColorAdjustments is empty.");
        }
    }
}