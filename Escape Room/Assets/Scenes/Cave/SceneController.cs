using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    private static float timer;
    private static float maxTime = 60.0f;

    public static SceneController instance;
    private static int crystalScore;
    private static int puzzlesCompleted;
    private static int maxPuzzles = 4;

    //public GameObject headstoneKeypad;
    //public GameObject furnaceKeypad;
    //public GameObject founderKeypad;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI completedText;
    public TextMeshProUGUI countdown;

    public GameObject crystal1;
    public GameObject crystal2;
    public GameObject crystal3;
    public GameObject crystal4;
    public GameObject crystal5;

    public static bool cr1B;
    public static bool cr2B;
    public static bool cr3B;
    public static bool cr4B;
    public static bool cr5B;

    //private static bool[] crystals = new bool[5];
    private static GameObject[] c;

    private static bool gameOver;
    private static bool crystalPuzzle;
    private static bool founder;
    private static bool headstone;
    private static bool furnace;

    public Rigidbody wallRB;
    public float wallInitialZ;
    public float wallFinalZ;

    //private int f = 3;
    //private int h = 4;
    //private int found = 6;
    private int win = 8;
    private int loss = 9;

    private Vector3 finalPos;
    private Vector3 initilPos;



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
        timer = 0;

        //Debug.Log("scene");

        gameOver = false;

        //c = new GameObject[5];

        //c[0] = crystal1;
        //c[1] = crystal2;
        //c[2] = crystal3;
        //c[3] = crystal4;
        //c[4] = crystal5;

        wallRB = wallRB.GetComponent<Rigidbody>();
        wallRB.transform.position = new Vector3(2.7f, 4.6f, wallInitialZ);

        if (true)
        {
            updatePuzzles(0);
        }
    }

    void Update()
    {
        //SetActiveCrystals();
        //SetCrystalBool();
        timer += Time.deltaTime;

        //crystals = CheckCrystalArray();
        //SetActiveCrystals();


        Check();

        //SetActiveCrystals();


        countdown.text = "TimeRemaining: " + ((int)(maxTime - timer)).ToString() + "s";
        scoreText.text = "Crystals Found: " + crystalScore.ToString() + "/5";
        completedText.text = "Puzzles Complete: " + puzzlesCompleted.ToString() + "/" + maxPuzzles.ToString();

        //GetActiveCrystalsAndSetBools();

        if (GetGameOver())
        {
            if (GetPuzzleScore() == maxPuzzles)
            {
                wallRB.transform.position = new Vector3(2.7f, 4.6f, wallFinalZ);
                SceneManager.LoadScene(win);
            }
            else
            {
                SceneManager.LoadScene(loss);
            }
        }
    }

    public void crystalPickup()
    {

        //GetActiveCrystalsAndSetBools();

        //Debug.Log(cr1B.ToString() + cr2B.ToString() + cr3B.ToString() + cr4B.ToString() + cr5B.ToString());

        if (gameOver == true)
        {
            return;
        }
        crystalScore++;
        if (crystalScore == 5)
        {
            crystalPuzzle = true;
            updatePuzzles();
        }
    }

    public void crystalPickup(GameObject obj)
    {

    }

    public void C()
    {
        Debug.Log(cr1B.ToString() + cr2B.ToString() + cr3B.ToString() + cr4B.ToString() + cr5B.ToString());
    }

    public void glowiDoorEnter()
    {
        if(gameOver == true && (puzzlesCompleted == maxPuzzles))
        {
            SceneManager.LoadScene(win);
        }
        else
        {
            Debug.Log("ChEAtEr");
            SceneManager.LoadScene(loss);
        }
    }

    public static bool isCrystalComplete()
    {
        return crystalPuzzle;
    }

    public void updatePuzzles()
    {
        puzzlesCompleted++;
        //completedText.text = "Puzzles Complete: " + puzzlesCompleted.ToString() + "/5";
    }

    public void updatePuzzles(int complete)
    {
        puzzlesCompleted = complete;
        //completedText.text = "Puzzles Complete: " + puzzlesCompleted.ToString() + "/5";
    }

    public static void CheckGameOver()
    {
        SetGameOver(false);

        if ((maxTime - timer) <= 0.0f)
        {
            SetGameOver(true);
            SetPuzzleScore(puzzlesCompleted);
            return;
        }

        //CheckCompletedPuzzles();

        if(puzzlesCompleted == maxPuzzles)
        {
            SetGameOver(true);
            SetPuzzleScore(puzzlesCompleted);
            return;
        }

        //SetGameOver(false);

    }

    public static void CheckCompletedPuzzles()
    {
        puzzlesCompleted = 0;
        if (founder)
        {
            puzzlesCompleted++;
        }

        if (headstone)
        {
            puzzlesCompleted++;
        }

        if (furnace)
        {
            puzzlesCompleted++;
        }

        if(crystalPuzzle)
        {
            puzzlesCompleted++;
        }
    }

    public static void Check()
    {
        //CheckCrystals();
        CheckCompletedPuzzles();
        CheckGameOver();
        CheckGameOverP();
    }

    public static void CheckGameOverP()
    {
        if(puzzlesCompleted == maxPuzzles)
        {
            SetGameOver(true);
        }
    }

    /*public static void CheckCrystals()
    {
        if(crystalScore == 5)
        {
            crystalPuzzle = true;
            crystals[0] = false;
            crystals[1] = false;
            crystals[2] = false;
            crystals[3] = false;
            crystals[4] = false;
        }
        else
        {
            crystalPuzzle = false;
        }
    }*/

    public bool CheckCrystalIndex(int index)
    {
        return c[index].activeSelf;
    }

    /*public static void SetCrystalIndex(int index, bool state)
    {
        crystals[index] = state;
        c[index].SetActive(state);
    }*/

    /*public bool[] CheckCrystalArray()
    {
        crystals = new bool[5];
        crystals[0] = CheckCrystalIndex(0);
        crystals[1] = CheckCrystalIndex(1);
        crystals[2] = CheckCrystalIndex(2);
        crystals[3] = CheckCrystalIndex(3);
        crystals[4] = CheckCrystalIndex(4);
        return crystals;
    }

    public static void SetCrystals(bool[] activity)
    {
        crystals[0] = activity[0];
        crystals[1] = activity[1];
        crystals[2] = activity[2];
        crystals[3] = activity[3];
        crystals[4] = activity[4];
    }*/

    public static void SetCrystalScore(int score)
    {
        crystalScore = score;
        if(score == 5)
        {
            crystalPuzzle = true;
        }
    }

    public static int GetCrystalScore()
    {
        return crystalScore;
    }

    public static void SetPuzzleScore(int score)
    {
        puzzlesCompleted = score;
        if(puzzlesCompleted == maxPuzzles)
        {
            SetGameOver(true);
        }
    }

    public static int GetPuzzleScore()
    {
        return puzzlesCompleted;
    }

    public static void SetGameOver(bool state)
    {
        gameOver = state;
    }

    public static bool GetGameOver()
    {
        return gameOver;
    }

    public static void SetKeypads(bool fds, bool fr, bool h)
    {
        furnace = fr;
        founder = fds;
        headstone = h;
    }

    public static bool GetFr()
    {
        return furnace;
    }

    public static bool GetFds()
    {
        return founder;
    }

    public static bool GetH()
    {
        return headstone;
    }

    public static void SetTimer(float time)
    {
        timer = time;
    }
    public static float GetTimer()
    {
        return timer;
    }

    /*public void SetActiveCrystals()
    {
        c[0].SetActive(crystals[0]);
        c[1].SetActive(crystals[1]);
        c[2].SetActive(crystals[2]);
        c[3].SetActive(crystals[3]);
        c[4].SetActive(crystals[4]);
    }

    public static bool[] GetActiveCrystals()
    {
        return crystals;
    }*/

    public bool GetCrystal(int index)
    {
        if (index == 0)
        {
            return crystal1.activeSelf;
        } else if (index == 1)
        {
            return crystal2.activeSelf;
        } else if (index == 2)
        {
            return crystal3.activeSelf;
        } else if (index == 3)
        {
            return crystal4.activeSelf;
        } else if (index == 4)
        {
            return crystal5.activeSelf;
        }
        else { return false; }
    }

    public static bool GetCrystalBool(int index)
    {
        if (index == 0)
        {
            return cr1B;
        }
        else if (index == 1)
        {
            return cr2B;
        }
        else if (index == 2)
        {
            return cr3B;
        }
        else if (index == 3)
        {
            return cr4B;
        }
        else if (index == 4)
        {
            return cr5B;
        }
        else { return false; }
    }

    public static void SetCrystalsBool(bool a, bool b, bool c, bool d, bool e)
    {
        cr1B = a;
        cr2B = b;
        cr3B = c;
        cr4B = d;
        cr5B = e;
    }

    public void SetActiveCrystals()
    {
        crystal1.SetActive(cr1B);
        crystal2.SetActive(cr2B);
        crystal3.SetActive(cr3B);
        crystal4.SetActive(cr4B);
        crystal5.SetActive(cr5B);
    }


    public static void UpdateSceneController(bool g, int p, int c, bool fds, bool fr, bool h, float time, bool c1, bool c2, bool c3, bool c4, bool c5)
    {
        SetGameOver(g);
        SetPuzzleScore(p);
        SetCrystalScore(c);
        SetKeypads(fds, fr, h);
        SetTimer(time);
        //SetCrystalsBool(c1, c2, c3, c4, c5);
    }

    public static void UpdateSceneController(bool g, int p, int c, bool fds, bool fr, bool h, float time)
    {
        SetGameOver(g);
        SetPuzzleScore(p);
        SetCrystalScore(c);
        SetKeypads(fds, fr, h);
        SetTimer(time);
        //SetCrystalsBool(c1, c2, c3, c4, c5);
    }

    public static bool GetActiveCrystals(int index)
    {
        return false;
    }

    public void SetActiveCry(Collider other)
    {
        if (other.CompareTag("C1"))
        {
            cr1B = false;
        }
        else if (other.CompareTag("C2"))
        {
            cr2B = false;
        }
        else if (other.CompareTag("C3"))
        {
            cr3B = false;
        }
        else if (other.CompareTag("C4"))
        {
            cr4B = false;
        }
        else if (other.CompareTag("C5"))
        {
            cr5B = false;
        }

        SetActiveCrystals();
    }

    /*
     SetUsingStaticMethod when restoring

     SetGameObject when Update
     Check()
     if pickedUP
     setBool to activeSelf
     */

    public void GetActiveCrystalsAndSetBools()
    {
        cr1B = crystal1.activeSelf;
        cr2B = crystal2.activeSelf;
        cr3B = crystal3.activeSelf;
        cr4B = crystal4.activeSelf;
        cr5B = crystal5.activeSelf;
    }

    public void SetCrystalBool() {
    }


}
