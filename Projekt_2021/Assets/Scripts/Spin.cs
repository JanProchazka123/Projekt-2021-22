using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spin : MonoBehaviour
{
    public GameObject toSpin;
    public float rotateX = 0;
    public float rotateY = 0;
    public float rotateZ = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        toSpin.transform.Rotate(rotateX,rotateY,rotateZ, Space.Self);
    }
}
