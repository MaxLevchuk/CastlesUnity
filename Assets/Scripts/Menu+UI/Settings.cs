using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class Settings : MonoBehaviour
{
    public static Settings Instance;
    public AudioMixer audioMixer;
    private ColorAdjustments colorAdjustments;
    public Volume GlobalVolume;

    private void Awake()
    {
        
        if (Instance == null)
        {
            Instance = this;

        }
        else
        {
            Destroy(gameObject); 
        }
    }
    void Start()
    {
        // Load saved settings when the game starts
        LoadSettings();
    }

    // Set the volume level
    public void SetVolume(float volume)
    {
        audioMixer.SetFloat("General Volume", volume);
        if (volume == -40)
        {
            audioMixer.SetFloat("General Volume", -80);
        }
    
    }

    // Set the brightness level
    public void SetBrightness(float brightness)
    {
        // Check if the GlobalVolume profile has ColorAdjustments component
        if (GlobalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            colorAdjustments.postExposure.value = brightness; // Adjust the brightness
        
        }
        else
        {
            Debug.LogWarning("ColorAdjustments is empty.");
        }
    }

    // Save the settings (volume and brightness)
    public void SaveSettings()
    {
        // Save volume level
        if (audioMixer.GetFloat("General Volume", out float volume))
        {
            PlayerPrefs.SetFloat("Volume", volume); // Store the current volume level
        }

        // Save brightness level
        if (GlobalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
        {
            PlayerPrefs.SetFloat("Brightness", colorAdjustments.postExposure.value); // Store the current brightness level
        }

        PlayerPrefs.Save(); // Ensure settings are saved to disk
    }

    // Load the saved settings
    public void LoadSettings()
    {
        // Load volume level if it has been saved
        if (PlayerPrefs.HasKey("Volume"))
        {
            float savedVolume = PlayerPrefs.GetFloat("Volume");
            audioMixer.SetFloat("General Volume", savedVolume); // Apply saved volume
        }

        // Load brightness level if it has been saved
        if (PlayerPrefs.HasKey("Brightness"))
        {
            float savedBrightness = PlayerPrefs.GetFloat("Brightness");
            // Apply saved brightness if ColorAdjustments component exists
            if (GlobalVolume.profile.TryGet<ColorAdjustments>(out colorAdjustments))
            {
                colorAdjustments.postExposure.value = savedBrightness;
            }
        }
    }
    public void UnlockLevels()
    {
        PlayerPrefs.SetInt("CurrentLevel", 29);
    }
    public void ResetLevels()
    {
        PlayerPrefs.SetInt("CurrentLevel", 2);
    }
}
