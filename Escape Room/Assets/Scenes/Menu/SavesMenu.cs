using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SavesMenu : MonoBehaviour
{
    public GameObject main;
    public GameObject saves;

    public void LoadMain()
    {
        main.SetActive(true);
        saves.SetActive(false);
    }
}
