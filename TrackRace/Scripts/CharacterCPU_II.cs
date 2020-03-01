using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCPU_II : MonoBehaviour
{
    private Rigidbody rb;
    private Animator anim;

    private bool isDead = false;
    private bool setChars = false;
    private bool raceStart = false;
    private float speed = 2.75f;
    private float lastPosition = 0.0f;
    private string pTag;
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
        lastPosition = rb.transform.position.x;
        if (isDead == false && gameController.instance.getRaceStart())
        {
            if (setChars == false)
            {
                setChars = true;
                anim.SetTrigger("raceSet");
            }

            if (raceStart == false && gameController.instance.getTime() > 0)
            {
                raceStart = true;
                anim.SetTrigger("raceStart");
            }

            if (gameController.instance.getTime() > 0)
            {
                Control(-2.75f);
            }

        }
    }

    void Control()
    {
        Control(1.0f);
    }
	
    void Control(float speed)
    {
        float moveHorizontal = 9.0f;
        if (rb.transform.position.y >= 9)
        {
            Vector3 movement = new Vector3(moveHorizontal, 10.0f, 0.0f);
            rb.AddForce(movement * speed);
        }
        else
        {
            Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
            rb.AddForce(movement * speed);
        }
    }

    void Jump()
    {
        anim.SetTrigger("isJumping");
        rb.velocity = Vector3.zero;
        float speed = 2.0f;
        float moveVertical = 200.0f;

        Vector3 movement = new Vector3(2.0f, moveVertical, 0.0f);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag.ToString() + " triggred");
        if (other.CompareTag("FINISH"))
        {
            rb.velocity = Vector3.zero;
            FinishLineAnim();
        }
        else if (other.CompareTag("Hurdle"))
        {
            Jump();
        }
    }
    /*void FinishLineAnim()
    {
        isDead = true;
        gameController.instance.setGameOver();
        rb.velocity = Vector3.zero;
        if (gameController.instance.getPlayerDone())
        {
            anim.SetTrigger("raceLoss");
        }
        else
        {

            anim.SetTrigger("raceWin");
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

