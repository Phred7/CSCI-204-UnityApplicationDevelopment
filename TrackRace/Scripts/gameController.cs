using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class gameController : MonoBehaviour
{
    public static gameController instance;
    public Text stopwatchText;
    public Text countDown;
    public GameObject camPlayer;
    public GameObject camIntro;

    AudioListener camPlayerAudioL;
    AudioListener camIntroAudioL;
    //private static float raceSetInterval = 6.0f;
    //public gameObject gameOverText;
    public bool isGameOver = false;
    public bool introPlaying = false;
    public bool raceStarted = false;
    public bool running = false;
    public bool playerDone = false;
    private float cpuTime = 10000f;
    private float introTime = 0.0f;
    private float time;
    private float auxTimer;


    private Dictionary<string, float> CPUCharConsts = new Dictionary<string, float>();
    
    private Rigidbody[] bodies = new Rigidbody[3];
    private Animator[] animators = new Animator[3];
    private float[] times = { 0.0f, 0.0f, 0.0f };
    private bool pos1 = false;
    private bool pos2 = false;
    private bool pos3 = false;
    private string[] tags = new string[3];
    private bool playersFinished = false;
    private float auxTimer2;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {

        CPUCharConsts.Add("knt_Jump", 150.0f);
        CPUCharConsts.Add("knt_Speed", -2.75f);
        CPUCharConsts.Add("arch_Jump", 200.0f);
        CPUCharConsts.Add("arch_Speed", -2.75f);

        time = -10.0f;
        auxTimer = 0.0f;
        auxTimer2 = 0.0f;

        introPlaying = false;
        playersFinished = false;

        switchToIntroCam();
    }

    // Update is called once per frame
    void Update()
    {
        if (getPlayersFinished())
        {
            //level changes automagically through LevelChanger.cs
            stopwatchText.text = "";
            countDown.text = "";
        }
        else
        {
            if (introTime <= 55.0f)
            {
                introTime += Time.deltaTime;
                introPlaying = true;
                stopwatchText.text = "";
                countDown.text = "";
            }
            else
            {
                introPlaying = false;
            }

            if (Input.anyKey && !introPlaying)
            {
                raceStarted = true;
            }

            if (introPlaying == false && raceStarted == false)
            {
                stopwatchText.text = "Race Time";
                countDown.text = "Trigger Any IO";
            }

            if (raceStarted == true && isGameOver == false)
            {
                switchToPlayerCam();
                time += Time.deltaTime;
                if (time < 0)
                {
                    startTime(time);
                }
                else
                {
                    countDown.text = "";
                    setRaceTime(time);
                    running = true;
                }

            }

            if (raceStarted == true && isGameOver == true && getPlayersFinished())
            {
                time += Time.deltaTime;
                if (playerDone)
                {
                    stopwatchText.text = ConvertTimeToString(auxTimer);
                }
            }
        }
        
    }

    public void startTime(float time)
    {
        int minutes = (int)(time / 60);
        //Debug.Log(time % 60f);
        if ((int)time % 60f == 0)
        {
            //Debug.Log(time);
            countDown.text = "GO!";
        }
        else
        {
            countDown.text = ConvertTimeToStringSeconds(time * -1);
        }
    }

    public void setRaceTime(float time)
    {
        stopwatchText.text = ConvertTimeToString(time);
    }
    string ConvertTimeToString(float time)
    {
        //Take the time and convert it into the number of minutes and seconds
        int minutes = (int)(time / 60);
        float seconds = time % 60f;

        string output = minutes.ToString("00") + ":" + seconds.ToString("00.000");
        return output;
    }

    string ConvertTimeToStringSeconds(float time)
    {
        //Take the time and convert it into the number of minutes and seconds
        int minutes = (int)(time / 60);
        float seconds = time % 60f;

        string output = seconds.ToString("0");
        return output;
    }
    public bool getRaceStart()
    {
        return raceStarted;
    }

    public bool getRunning()
    {
        return running;
    }

    public float getTime()
    {
        return time;
    }

    public bool setGameOver()
    {
        isGameOver = true;
        return isGameOver;
    }
    public bool gameOver()
    {
        return isGameOver;
    }

    public bool getPlayerWin(float lapTime, float cpuTime)
    { 
        if (lapTime <= cpuTime)
        {
            auxTimer = lapTime;
            return true;
        }
        else
        {
            auxTimer = cpuTime;
            return false;
        }
    }

    public void setPlayerDone(bool set)
    {
        playerDone = set;
    }

    public bool getPlayerDone()
    {
        return playerDone;
    }

    public void setCPUTime(float time)
    {
        cpuTime = time;
    }

    public float getCPUTime()
    {
        return cpuTime;
    }

    public void switchToIntroCam()
    {
        camIntro.SetActive(true);
       // camIntroAudioL.enabled = true;

       // camPlayerAudioL.enabled = false;
        camPlayer.SetActive(false);
    }

    public void switchToPlayerCam()
    {
        camPlayer.SetActive(true);
       // camPlayerAudioL.enabled = true;

       // camIntroAudioL.enabled = false;
        camIntro.SetActive(false);
    }

    public string[] getPositionTags()
    {
        return tags;
    }

    public float getCPUConsts(string key)
    {
        return CPUCharConsts[key];
    }

    //gameController
    public void setPlayersFinished(bool finish)
    {
        playersFinished = finish;
    }

    //gameController
    public bool getPlayersFinished()
    {
        return playersFinished;
    }

    //gameController
    /*public int setFinishTime(float time)
    {
        if (times[0] == 0.0f)
        {
            return 0;
        }
        else if (times[1] == 0.0f)
        {
            return 1;
        }
        else
        {
            auxTimer = time;
            setPlayersFinished(true);
            Debug.Log("Players have finished and times recorded \n time 3: " + ConvertTimeToString(times[2]));
            return 2;
        }
    }*/

    public int setFinishTime(float time)
    {
        if (!pos1)
        {
            pos1 = true;
            times[0] = time;
            return 0;
        }
        else if (!pos2)
        {
            pos2 = true;
            times[1] = time;
            return 1;
        }
        else
        {
            pos3 = true;
            times[2] = time;
            auxTimer = time;
            Debug.Log("Players have finished and times recorded \n" + "time 1: " + ConvertTimeToString(times[0])  + " time 2: " + ConvertTimeToString(times[1]) + " time 3: " + ConvertTimeToString(times[2]));
            return 2;
        }
    }

    //gameController
    public void setBodies(int pos, Rigidbody rb, Animator anim, string pTag)
    {
        if (tags[0] == pTag || tags[1] == pTag || tags[2] == pTag)
        {
            Debug.Log("This player: " + pTag + "has already been recorded");
            //times[pos] = 0.0f;
            setPosBools(pos, false);
        }
        else
        {
            bodies[pos] = rb;
            animators[pos] = anim;
            tags[pos] = pTag;
            if (pos == 2)
            {
                setPlayersFinished(true);
            }
            Debug.Log((pos + 1) + ": " + pTag + " finished and was set in bodies with a time of: " + ConvertTimeToString(times[pos]));
        }
        
    }

    void setPosBools(int pos, bool value)
    {
        if(pos == 0)
        {
            pos1 = value;
        }else if (pos == 1)
        {
            pos2 = value;
        }
        else if (pos == 2)
        {
            pos3 = value;
        }
    }

    public float[] getTimes()
    {
        return times;
    }

    public float getTimes(int pos)
    {
        if(pos < 0 || pos > 2)
        {
            return -10.0f;
        }
        else
        {
            return times[pos];
        }

    }
}
