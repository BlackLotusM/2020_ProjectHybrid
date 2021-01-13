using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeScript : MonoBehaviour
{
    public AudioMixer am;
    public Slider slider;

    public void setVolume(float volume)
    {
        am.SetFloat("MasterVolume", volume);
    }
}
