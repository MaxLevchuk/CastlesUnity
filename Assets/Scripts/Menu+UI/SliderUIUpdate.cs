using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUpdate : MonoBehaviour
{
    Slider slider;
    public string SettingsName;
    void Start()
    {
        slider = GetComponent<Slider>();
        if (PlayerPrefs.HasKey(SettingsName))
        {
            float savedVolume = PlayerPrefs.GetFloat(SettingsName);
            slider.value = savedVolume;
        }
    }

}
