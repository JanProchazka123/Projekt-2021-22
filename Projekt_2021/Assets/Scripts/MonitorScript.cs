using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonitorScript : MonoBehaviour
{
    public Animator m1;
    public Animator m2;
    public Animator m3;
    public Animator m4;
    public Animator m5;
    public bool yesorno = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (yesorno == false)
        {
            m1.SetInteger("killme", 0);
            m2.SetInteger("killme", 0);
            m3.SetInteger("killme", 0);
            m4.SetInteger("killme", 0);
            m5.SetInteger("killme", 0);
        }
        else
        {
            m1.SetInteger("killme", 1);
            m2.SetInteger("killme", 1);
            m3.SetInteger("killme", 1);
            m4.SetInteger("killme", 1);
            m5.SetInteger("killme", 1);
        }
    }
}
