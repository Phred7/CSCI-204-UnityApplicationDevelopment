using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCPU : MonoBehaviour
{
    // Start is called before the first frame update

    private Rigidbody rb;
    private Animator anim;
	private string tag;

    private bool isDead = false;
    private bool setChars = false;
    private bool raceStart = false;
    //private float speed = 2.75f;
    //private float lastPosition = 0.0f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //lastPosition = rb.transform.position.x;
        if(isDead == false && gameController.instance.getRaceStart())
        {
            if(setChars == false)
            {
                setChars = true;
                anim.SetTrigger("raceSet");
            }

            if (raceStart == false && gameController.instance.getTime() > 0)
            {
                raceStart = true;
                anim.SetTrigger("raceStart");
            }

            if(gameController.instance.getTime() > 0)
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
        float moveHorizontal = 10.0f;

        Vector3 movement = new Vector3(moveHorizontal, 0.0f, 0.0f);
        rb.AddForce(movement * speed);
    }

    void Jump()
    {
        anim.SetTrigger("isJumping");
        rb.velocity = Vector3.zero;
        float speed = 2.0f;
        float moveVertical = 200.0f;

        Vector3 movement = new Vector3(10.0f, moveVertical, 0.0f);
        rb.AddForce(movement * speed);
    }

    void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.tag.ToString() + " triggred");
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
    void FinishLineAnim()
    {
        isDead = true;
        gameController.instance.setGameOver();
        rb.velocity = Vector3.zero;
        gameController.instance.setCPUTime(gameController.instance.getTime());
        if (gameController.instance.getPlayerDone())
        {
            anim.SetTrigger("raceLoss");
        }
        else
        {
            
            anim.SetTrigger("raceWin");
        }
    }
}

