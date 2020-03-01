using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTrigger : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rigid = null;

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<bird>() != null)
        {
            GameControl.instance.CoinScored();
            //Destroy(rigid);
            //Destroy(this);
            Destroy(gameObject);
        }
    }
}
