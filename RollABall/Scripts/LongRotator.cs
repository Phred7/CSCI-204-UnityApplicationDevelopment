using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LongRotator : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        transform.Rotate(new Vector3(25, 0, 0) * Time.deltaTime);
    }
}
