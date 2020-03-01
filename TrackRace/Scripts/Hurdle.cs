using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hurdle : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        //Debug.Log(other.tag.ToString() + " Entered Hurdle");
        if (other.GetComponent<GameObject>() != null)
        {
            //Debug.Log(other.tag.ToString() + " Entered Hurdle");
            if (other.GetComponent<GameObject>().CompareTag("Player"))
            {
                other.GetComponent<Rigidbody>().AddForce(0.0f, 100000f, 0.0f);
            }
        }
        
        //other.GetComponent<GameObject>().CompareTag("Player");
    }

    /*private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("pre-null");

        /*if(other.collider.name == "Beam")
        {

        }

        if(other.GetComponent<Collider>().name == "Beam")
        {
            Debug.Log("Collision");
        }
        if (other.GetComponent<CharacterCPU>() != null)
            
        {
            Debug.Log("Existence");
            other.GetComponent<CharacterCPU>().Jump();
            if (other.GetComponent<CharacterCPU>().CompareTag("CharacterKnight"))
            {
                Debug.Log("Knight");
            }
            else if (other.GetComponent<CharacterCPU>().CompareTag("CharacterArcher"))
            {
                Debug.Log("Archer");
            }
            else
            {
                Debug.Log("NO");
            }
        }
    }*/
}
