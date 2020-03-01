using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    /**
     * Menus
     */
    public GameObject main;
    public GameObject options;
    public GameObject credits;
    public GameObject saves;

    public GameObject background;
    public GameObject blackScreen;

    void Start()
    {
        main.SetActive(true);
        background.SetActive(true);

        options.SetActive(false);
        credits.SetActive(false);
        blackScreen.SetActive(false);
    }

    public void PlayGame()
    {
        LoadNewGame();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadOptionsMenu()
    {
        main.SetActive(false);
        options.SetActive(true);
        float cT = 0;

    }

    public void CloseScene()
    {
        Debug.Log("QUITTING GAME");
        main.SetActive(false);
        options.SetActive(false);
        credits.SetActive(true);
        background.SetActive(false);
        blackScreen.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        SaveGame();
        Application.Quit();
    }

    public void LoadGameFromSave()
    {
        main.SetActive(false);
        saves.SetActive(true);
        SaveLoad.Load();   
    }

    void SaveGame()
    {
        SaveData save = new SaveData();
        save.x = 11.2f;
        save.y = -0.4f;
        save.z = 4.45f;

        string json = JsonUtility.ToJson(save);
        Debug.Log(json);
        PlayerPrefs.SetString("Player", json);
    }

    public void LoadNewGame()
    {
        SaveData save = new SaveData();
        Vector3 vec = new Vector3(11.2f, -0.4f, 4.45f);
        save.x = vec.x;
        save.y = vec.y;
        save.z = vec.z;
        save.xR = 0;
        save.yR = 0;
        save.zR = 0;
        save.currTime = 0.0f;
        save.score = 0;
        save.crystalsScore = 0;
        save.furnaces = false;
        save.founders = false;
        save.headstone = false;
        save.gameOver = false;

        string json = JsonUtility.ToJson(save);
        Debug.Log("MenuReset: " + json);
        PlayerPrefs.SetString("Player", json);
    }
}
