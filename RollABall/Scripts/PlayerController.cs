using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;

    public float speed;

    void Start()
    {
        rb.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float vertical = Input.GetAxis("Vertical");
        float horizontal = Input.GetAxis("Horizontal"); //Grabs the horizontal axis
        Debug.Log(horizontal); //same as a print statement to see what its doing


        Vector3 position = transform.position;

        position.x = position.x + speed * horizontal * Time.deltaTime * -1;
        position.z = position.z + speed * vertical * Time.deltaTime * -1;
        transform.position = position;
    }
}
