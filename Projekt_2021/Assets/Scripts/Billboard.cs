using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    public GameObject cam;

    private void Awake()
    {
        //billboard hledá GameObject kterej se jmenuje pøesnì PlayerCamera - to by se mohlo rozbít
        cam = GameObject.Find("Player Camera");
    }

    void LateUpdate()
    {
        transform.LookAt(transform.position + cam.transform.forward);
    }
}
