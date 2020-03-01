using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class podiumController : MonoBehaviour
{
    public static podiumController instance;

    public GameObject player;
    public GameObject archer;
    public GameObject knight;

    public Text fText;
    public Text sText;
    public Text tText;

    private float time;
    /*
    private Rigidbody pRB;
    private Rigidbody aRB;
    private Rigidbody kRB;
    private Animator pAnim;
    private Animator aAnim;
    private Animator kAnim;
    */

    private GameObject[] bodies = new GameObject[3];
    private Vector3[] podiumPositions = { new Vector3(-867.5f, 26.8f, 0.0f), new Vector3(-867.5f, 26.8f, -10.0f), new Vector3(-867.5f, 26.8f, 10.0f) };
    private float[] times = new float[3];


    // Start is called before the first frame update
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
        /*
        pRB = GetComponent<Rigidbody>();
        pAnim = GetComponent<Animator>();

        aRB = GetComponent<Rigidbody>();
        aAnim = GetComponent<Animator>();

        kRB = GetComponent<Rigidbody>();
        kAnim = GetComponent<Animator>();
        */

        fText.text = "";
        sText.text = "";
        tText.text = "";

        time = 0.0f;
        
        getPositions();
        teleportPlayers();

    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(time > 32.0f)
        {
            fText.text = "1st:\t" + ConvertTimeToString(times[0]);
            sText.text = "2nd:\t" + ConvertTimeToString(times[1]);
            tText.text = "3rd:\t" + ConvertTimeToString(times[2]);
        }
    }

    private void getPositions()
    {
        string[] tags = gameController.instance.getPositionTags();
        for (int i = 0; i <= 2; i++)
        {
            if (tags[i] == "Player")
            {
                bodies[i] = player;
            }else if (tags[i] == "CharacterArcher")
            {
                bodies[i] = archer;
            }
            else if (tags[i] == "CharacterKnight")
            {
                bodies[i] = knight;
            }
            else
            {

                //Debug.Log("BodiesArrayCreationFailed \n Trace: podiumController");
                //Debug.Log("Tags: " + i);
                //Debug.Log(tags[0]);
                break;
            }
            Debug.Log(tags[i]);
        }

    }

    private void teleportPlayers()
    {
        getTimes(0);
        getTimes(1);
        getTimes(2);

        bodies[0].transform.position = podiumPositions[0];
        bodies[1].transform.position = podiumPositions[1];
        //Debug.Log(bodies[2].ToString());
        bodies[2].transform.position = podiumPositions[2];

        bodies[0].GetComponent<Animator>().SetTrigger("Win");
        bodies[1].GetComponent<Animator>().SetTrigger("Loss");
        bodies[2].GetComponent<Animator>().SetTrigger("Loss");
    }

    private string ConvertTimeToString(float time)
    {
        //Take the time and convert it into the number of minutes and seconds
        int minutes = (int)(time / 60);
        float seconds = time % 60f;

        string output = minutes.ToString("00") + ":" + seconds.ToString("00.000");
        return output;
    }

    private void getTimes()
    {
        times = gameController.instance.getTimes();
    }
    public float getTime()
    {
        return time;   
    }

    private void getTimes(int pos)
    {
        times[pos] =  gameController.instance.getTimes(pos);
    }
}
