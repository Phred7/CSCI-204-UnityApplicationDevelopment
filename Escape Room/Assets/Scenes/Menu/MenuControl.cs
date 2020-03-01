using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuControl : MonoBehaviour
{
    public GameObject main;
    public GameObject options;
    public GameObject credits;
    public GameObject saves;

    public GameObject background;
    public GameObject blackScreen;

    public GameObject music;

    void Start()
    {
        main.SetActive(true);
        background.SetActive(true);
        music.SetActive(true);

        options.SetActive(false);
        credits.SetActive(false);
        saves.SetActive(false);
        blackScreen.SetActive(false);
    }
}
