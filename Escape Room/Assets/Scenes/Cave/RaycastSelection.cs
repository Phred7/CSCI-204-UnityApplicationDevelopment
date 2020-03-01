using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class RaycastSelection : MonoBehaviour
{
    public GameObject player;
    //public GameObject _scenecontroller;

    public float force = 5;

    private int furnaceScene = 3;
    private int headstoneScene = 4;
    private int founderScene = 6;

    private int founderCanvas = 7;
    private int headstoneCanvas = 5;

    private int backButtonScene = 1;

    void Start()
    {
        RestoreGame();
        //Debug.Log("rays");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //Debug.Log("Click");
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(ray, out hit, 100.0f))
            {
                //Debug.Log("Ray");
                if (hit.transform != null)
                {
                    

                    Rigidbody rb;

                    //Debug.Log("!null");

                    if (rb = hit.transform.GetComponent<Rigidbody>())
                    {
                        //Debug.Log("rigid" + rb.tag);
                        if (rb.gameObject.CompareTag("FurnaceKey"))
                        {
                            PrintName(hit.transform.gameObject);
                            Load(furnaceScene);
                        }
                        else if(rb.gameObject.CompareTag("HeadstoneKey"))
                        {
                            PrintName(hit.transform.gameObject);
                            Load(headstoneScene);
                        }
                        else if (rb.gameObject.CompareTag("FoundersKey"))
                        {
                            PrintName(hit.transform.gameObject);
                            Load(founderScene);
                        }
                        else if (rb.gameObject.CompareTag("Headstone"))
                        {
                            PrintName(hit.transform.gameObject);
                            Load(headstoneCanvas);
                        }
                        else if (rb.gameObject.CompareTag("Founders"))
                        {
                            PrintName(hit.transform.gameObject);
                            Load(founderCanvas);
                        }
                        else
                        {
                            //Debug.Log("noscene: " + );
                        }

                    }
                }
            }
        }
    }

    private void PrintName(GameObject go)
    {
        //print(go.name);
    }

    private void LaunchIntoAir(Rigidbody rig)
    {
        rig.AddForce(rig.transform.up * force, ForceMode.Impulse);
    }

    private void Load(int scene)
    {
        SaveGame();
        SceneManager.LoadScene(scene);
    }

    void SaveGame()
    {
        SaveData save= new SaveData();
        save.x = player.transform.position.x;
        save.y = player.transform.position.y;
        save.z = player.transform.position.z;
        save.xR = player.transform.localEulerAngles.x;
        save.yR = player.transform.localEulerAngles.y;
        save.zR = player.transform.localEulerAngles.z;
        save.currTime = SceneController.GetTimer();
        save.score = SceneController.GetPuzzleScore();
        save.crystalsScore = SceneController.GetCrystalScore();
        save.furnaces = SceneController.GetFr();
        save.founders = SceneController.GetFds();
        save.headstone = SceneController.GetH();
        save.gameOver = SceneController.GetGameOver();
        /*save.c1 = SceneController.GetCrystalBool(0);
        save.c2 = SceneController.GetCrystalBool(1);
        save.c3 = SceneController.GetCrystalBool(2);
        save.c4 = SceneController.GetCrystalBool(3);
        save.c5 = SceneController.GetCrystalBool(4);*/


        string json = JsonUtility.ToJson(save);
        Debug.Log("RaySaving: " + json);
        PlayerPrefs.SetString("Player", json);
    }

    void RestoreGame()
    {
        string save = PlayerPrefs.GetString("Player");
        if(save != null && save.Length > 0)
        {
            
            SaveData data = JsonUtility.FromJson<SaveData>(save);
            Debug.Log("RayRestoring: " + save);
            if (data != null)
            {

                Vector3 rot = new Vector3();
                rot.x = data.xR;
                rot.y = data.yR;
                rot.z = data.zR;
                player.transform.localEulerAngles = rot;

                Vector3 pos = new Vector3();
                pos.x = data.x;
                pos.y = data.y;
                pos.z = data.z;

                if(data.x <= 0)
                {
                    pos = new Vector3(11.2f, -0.4f, 4.45f);
                }

                player.transform.position = pos;

                SceneController.UpdateSceneController(data.gameOver, data.score, data.crystalsScore, data.founders, data.furnaces, data.headstone, data.currTime);

                /*if (data.c1)
                {
                    data.c1 = false;
                }
                else
                {
                    data.c1 = true;
                }

                if (data.c2)
                {
                    data.c2 = false;
                }
                else
                {
                    data.c2 = true;
                }

                if (data.c3)
                {
                    data.c3 = false;
                }
                else
                {
                    data.c3 = true;
                }

                if (data.c4)
                {
                    data.c4 = false;
                }
                else
                {
                    data.c4 = true;
                }

                if (data.c5)
                {
                    data.c5 = false;
                }
                else
                {
                    data.c5 = true;
                }

                SceneController.UpdateSceneController(data.gameOver, data.score, data.crystalsScore, data.founders, data.furnaces, data.headstone, data.currTime, data.c1, data.c2, data.c3, data.c4, data.c5);
                */
            }
            else
            {
                Debug.Log("Your data could not be loaded");
            }
        }
        else
        {
            Debug.Log("Your save was corrupted or did not exist");
            player.transform.position = new Vector3(11.2f, -0.4f, 4.45f);
            player.transform.localEulerAngles = new Vector3();
        }
    }

}
