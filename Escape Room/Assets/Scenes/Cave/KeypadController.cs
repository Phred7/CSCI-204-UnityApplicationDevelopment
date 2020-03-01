using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class KeypadController : MonoBehaviour
{
    public GameObject sigilsImg;
    public bool sigils;

    private static float timer;

    public int backButtonScene;

    public static int score;

    public TextMeshProUGUI text;
    public Image bar;
    private Color curr;

    public int charSpacing;

    public int maxChar;
    public int password;
    private int passLength;

    private int inputs;
    private int tracker;

    private static bool completeF;
    private static bool completeH;
    private static bool completeFound;
    private int activeScene;
    private static int f = 3;
    private static int h = 4;
    private static int found = 6;

    /*void Awake()
    {
        GameObject[] furn = GameObject.FindGameObjectsWithTag("KeyPadfurn");
        GameObject[] f = GameObject.FindGameObjectsWithTag("KeyPadf");
        GameObject[] h = GameObject.FindGameObjectsWithTag("KeyPadh");

        if (furn.Length > 1 && this.gameObject.CompareTag("KeyPadfurn"))
        {
            Destroy(this.gameObject);
        }
        else if (f.Length > 1 && this.gameObject.CompareTag("KeyPadf"))
        {
            Destroy(this.gameObject);
        }
        else if (h.Length > 1 && this.gameObject.CompareTag("KeyPadh"))
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }*/

    void Start()
    {

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;

        score = 0;

        curr = bar.color;
        passLength = maxChar;
        text.characterSpacing = charSpacing;
        inputs = 0;
        tracker = 0;
        activeScene = getCurrentScene();
        sigilsImg.SetActive(sigils);

        if (isComplete(activeScene))
        {
            curr = Color.green;
            bar.color = curr;
            text.text = password.ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Screen.lockCursor = false;
        

        if (passLength < 1)
        {
            Debug.Log("Null Password Length");
        }else if (maxChar < 1)
        {
            Debug.Log("Null Code Char Limit");
        }

        timer += Time.deltaTime;

    }


    public void AddChar(int input)
    {
        if (isComplete(activeScene))
        {
            Debug.Log("Already Complete");
        }
        else if (tracker < passLength)
        {
            inputs *= 10;
            inputs += input;
            tracker++;
            UpdateText();
        }
        else if(tracker == passLength)
        {
            Clear();
            AddChar(input);
        }
    }

    public void Clear()
    {
        if (isComplete(activeScene))
        {
            Debug.Log("Already Complete");
        }
        else
        {
            inputs = 0;
            tracker = 0;
            UpdateText();
        }
    }

    public void Enter()
    {
        if (isComplete(activeScene))
        {
            bar.color = Color.green;
        }
        else
        {
            if (password == inputs)
            {
                bar.color = Color.green;
                setComplete(activeScene);
                //Debug.Log(isComplete(activeScene));
            }
            else
            {
                bar.color = Color.red;
            }
        }
        
    }

    public void UpdateText()
    {
        if (isComplete(activeScene))
        {
            Debug.Log("Already Complete");
        }
        else if (tracker == 0)
        {
            text.text = "CODE";
        }
        else
        {
            bar.color = curr;
            if(inputs == 0)
            {
                if(tracker == 1)
                {
                    text.text = "0";
                }else if(tracker == 2)
                {
                    text.text = "00";
                }
                else if (tracker == 3)
                {
                    text.text = "000";
                }
                else if (tracker == 4)
                {
                    text.text = "0000";
                }
            }/*else if ((float)(((float)inputs)/Mathf.Pow(10, tracker)) < Mathf.Pow(10, tracker))
            {
                if (tracker == 2)
                {
                    text.text = "0" + inputs.ToString();
                }
                else if (tracker == 3)
                {
                    text.text = "00" + inputs.ToString();
                }
                else if (tracker == 4)
                {
                    text.text = "000" + inputs.ToString();
                }
            }*/
            else
            {
                text.text = inputs.ToString();
            }
        }
    }

    public static bool isComplete(int scene)
    {
        if (scene > 0)
        {
            if (scene == f)
            {
                //Debug.Log(completeF);
                return completeF;
            }
            else if (scene == h)
            {
                //Debug.Log(completeH);
                return completeH;
            }
            else if (scene == found)
            {
                //Debug.Log(completeFound);
                return completeFound;
            }
            else { return false; }
        }
        else { return false; }
    }

    public static bool allComplete()
    {
        if(score == 3)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static void setComplete(int scene)
    {
        if(getScore() == 3)
        {
            return;
        }

        if (scene == f && !completeF)
        {
            completeF = true;
            score++;
        }
        else if (scene == h && !completeH)
        {
            completeH = true;
            score++;
        }
        else if (scene == found && !completeFound)
        {
            completeFound = true;
            score++;
        }
        else {}
    }

    private static void CheckScore()
    {
        if (completeF && completeFound && completeH)
        {
            score = 3;
            return;
        }
        else if (completeF && completeFound || completeF && completeH || completeH && completeFound)
        {
            score = 2;
        }
        else if (completeF || completeFound || completeH)
        {
            score = 1;
        }
        else
        {
            score = 0;
        }
    }

    private static int getScore()
    {
        CheckScore();
        return score;
    }

    public void returnToScene()
    {
        UpdateSave();
        SceneManager.LoadScene(backButtonScene); //1 for cave, 2 for couryard
    }

    public int getCurrentScene()
    {
        return SceneManager.GetActiveScene().buildIndex;
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


                pos.score = getScore();
                pos.founders = completeFound;
                pos.furnaces = completeF;
                pos.headstone = completeH;

                

                string json = JsonUtility.ToJson(pos);
                Debug.Log("UpdatingSave:\nLastSave: " + save + "\nNewSave: " + json);
                PlayerPrefs.SetString("Player", json);
            }
        }
    }

    private void UpdateKeypad()
    {

    }
}
