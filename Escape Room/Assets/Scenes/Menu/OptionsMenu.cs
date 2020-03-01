using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject options;
    public GameObject bkg;
    public GameObject bkgF;
    public AudioMixer audio;
    //public Slider slider;

    public void LoadMain()
    {
        main.SetActive(true);
        options.SetActive(false);
        bkg.SetActive(true);
        bkgF.SetActive(false);
    }

    public void SetVolume (float volume)
    {
        audio.SetFloat("Volume", volume);
    }
}
