using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrystalTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rigid = null;

    /*void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Crystal");
        if (objs.Length > 5)
        {
            Destroy(this.gameObject);


        }
        DontDestroyOnLoad(this.gameObject);
    }*/

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>() != null)
        {
            SceneController.instance.crystalPickup();
            //SceneController.instance.SetActiveCry(other);
            gameObject.SetActive(false);
            //SceneController.instance.C();
        }
    }

}
