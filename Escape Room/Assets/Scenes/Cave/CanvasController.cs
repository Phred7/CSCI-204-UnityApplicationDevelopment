using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasController : MonoBehaviour
{

    private float timer;

    private int backButtonScene = 1;

    void Start()
    {
        timer = 0;
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    void Update()
    {
        timer += Time.deltaTime;
    }

    public void returnToScene()
    {
        SceneManager.LoadScene(backButtonScene); //1 for cave, 2 for couryard
    }

    private void UpdateSave()
    {
        string save = PlayerPrefs.GetString("Player");
        if (save != null && save.Length > 0)
        {
            SaveData data = JsonUtility.FromJson<SaveData>(save);
            if (data != null)
            {
                SaveData pos = new SaveData();

                pos.xR = data.xR;
                pos.yR = data.yR;
                pos.zR = data.zR;
                pos.x = data.x;
                pos.y = data.y;
                pos.z = data.z;
                pos.crystalsScore = data.crystalsScore;
                pos.gameOver = data.gameOver;
                pos.currTime = data.currTime + timer;
                /*pos.c1 = data.c1;
                pos.c2 = data.c2;
                pos.c3 = data.c3;
                pos.c4 = data.c4;
                pos.c5 = data.c5;*/
                pos.score = data.score;
                pos.founders = data.founders;
                pos.furnaces = data.furnaces;
                pos.headstone = data.headstone;
            

                string json = JsonUtility.ToJson(pos);
                Debug.Log("UpdatingSave:\nLastSave: " + save + "\nNewSave: " + json);
                PlayerPrefs.SetString("Player", json);
            }
        }
    }
}
