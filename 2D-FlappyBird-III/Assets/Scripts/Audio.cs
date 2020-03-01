using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audio : MonoBehaviour
{

    public AudioClip music;
    public AudioClip coin;

    public AudioSource musicSource;
    public AudioSource coinSource;

    // Start is called before the first frame update
    void Start()
    {
        musicSource.clip = music;
        musicSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        if(GameControl.instance.gameOver == true)
        {
            musicSource.Stop();
        }
        else if (!musicSource.isPlaying)
        {
            musicSource.Play();
        }
    }
}
