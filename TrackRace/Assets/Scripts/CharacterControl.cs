using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterControl : MonoBehaviour
{
    public bool isDead = false;
    private bool setCharacters = false;
    private bool raceStart = false;
    private Rigidbody rb;
    private Animator anim;
    private string pTag;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        pTag = GetComponent<Collider>().tag;
        Debug.Log(pTag);
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead == false && gameController.instance.getRaceStart())
        {
            if (setCharacters == false)
            {
                if (Input.anyKey)
                {
                    setCharacters = true;
                    anim.SetTrigger("raceSet");
                }
            }

            if(raceStart == false && gameController.instance.getTime() > 0)
            {
                raceStart = true;
                anim.SetTrigger("raceStart");
                rb.AddForce(-150.0f, 0.0f, 0.0f);
            }

            if (raceStart && Input.GetKeyDown("space"))
            {
                anim.SetTrigger("isJumping");

                if (rb.transform.position.y >= 8)
                {
                    rb.AddForce(new Vector3(0.0f, -1200.0f, 0));
                    //Debug.Log("down");
                }
                else
                {
                    rb.AddForce(new Vector3(0.0f, 600.0f, 0.0f));
                }

            }

            if (raceStart && rb.velocity.x < 8.0f && rb.velocity.x != 0)
            {
                anim.SetBool("walking", true);
                anim.SetBool("idle", false);
            }
            else if (raceStart && rb.velocity.x == 0 && rb.velocity.y < 5.0f)
            {
                anim.SetBool("idle", true);
            }
            else
            {
                anim.SetBool("walking", false);
            }

            if (raceStart && Input.GetMouseButtonDown(0))
            {
                //Debug.Log("ClickDetected");
                if(rb.transform.position.y >= 10)
                {
                    rb.AddForce(new Vector3(-100.0f, -1200.0f, 0));
                    //Debug.Log("down");
                }
                else
                {
                    //Debug.Log("new force");
                    rb.AddForce(new Vector3(-350.0f, 0.0f, 0));
                }
            }


        }
    }

    void OnCollisionEnter(Collision other)
    {
        /*if (other.CompareTag("FINISH"))
        {
            rb.velocity = Vector3.zero;
            if (gameController.instance.gameOver())
            {
                anim.SetTrigger("raceWin");
            }
            else
            {
                anim.SetTrigger("raceLoss");
                float time = 0.0f;
                rb.AddForce(new Vector3(-100.0f, 0.0f, 0.0f));
                while (time < 5)
                {
                    time += Time.deltaTime;
                }
                anim.SetTrigger("zWalk");
                time = 0.0f;
                rb.AddForce(new Vector3(25.0f, 0.0f, 0.0f));
                while (time < 3)
                {
                    time += Time.deltaTime;
                }
                rb.velocity = Vector3.zero;
                isDead = true;
                anim.SetTrigger("death");
            }
        }
        else
        {

        }*/
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag.ToString() + " triggred");
        if (other.CompareTag("FINISH"))
        {
            rb.velocity = Vector3.zero;
            FinishLineAnim();
        }else if (other.CompareTag("Hurdle")){
            float x = rb.velocity.x;
            x *= -2;
            rb.velocity = Vector3.zero;
            //rb.AddForce(new Vector3(x, 0.0f, 0.0f));
            //Debug.Log("FORCED");
        }
    }

    /*void FinishLineAnim()
    {
        isDead = true;
        gameController.instance.setPlayerDone(true);
        float lapTime = gameController.instance.getTime();
        gameController.instance.setGameOver();
        if (gameController.instance.getPlayerWin(lapTime, gameController.instance.getCPUTime()))
        {
            anim.SetTrigger("raceWin");
        }
        else
        {
            anim.SetTrigger("raceLoss");
            float time = 0.0f;
            rb.AddForce(new Vector3(-100.0f, 0.0f, 0.0f));
            while (time < 5)
            {
                time += Time.deltaTime;
            }
            anim.SetTrigger("zWalk");
            time = 0.0f;
            rb.AddForce(new Vector3(25.0f, 0.0f, 0.0f));
            while (time < 3)
            {
                time += Time.deltaTime;
            }
            rb.velocity = Vector3.zero;
            anim.SetTrigger("death");
        }
    }*/

    void FinishLineAnim()
    {
        int finishPos = gameController.instance.setFinishTime(gameController.instance.getTime()); //sends time to gameController and determines what place this character came in
        isDead = true;
        if (finishPos == 0)
        { //win 
            gameController.instance.setBodies(0, rb, anim, pTag);
            win();
        }
        else if (finishPos == 1)
        { //2nd
            gameController.instance.setBodies(1, rb, anim, pTag);
            loss();
        }
        else if (finishPos == 2)
        { //3rd
            gameController.instance.setBodies(2, rb, anim, pTag);
            loss();
        }
        else
        { //broken

        }
    }

    void win()
    {
        anim.SetTrigger("raceWin");
    }

    void loss()
    {
        anim.SetTrigger("raceLoss");
    }
}
