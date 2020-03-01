using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSLider : MonoBehaviour
{
    // Start is called before the first frame update
    public Slider Volume;
    public AudioSource music;


    // Update is called once per frame

    void Start()
    {
        Volume.value = Volume.maxValue;
    }
    void Update()
    {

    }
    public void SetVolume()
    {
        music.volume = Volume.value;
    }
}
