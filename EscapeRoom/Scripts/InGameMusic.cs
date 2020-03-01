using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMusic : MonoBehaviour
{
    private static float volume;
    public AudioSource music;

    // Start is called before the first frame update
    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if(objs.Length > 1)
        {
            Destroy(this.gameObject);

            
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        setVol(music.volume);
    }

    public static void setVol(float vol)
    {
        volume = vol;
    }

    public static float getVol()
    {
        return volume;
    }


}
